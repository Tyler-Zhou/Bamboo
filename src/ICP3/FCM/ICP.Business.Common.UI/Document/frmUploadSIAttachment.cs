using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.UserSkins;
using ICP.Business.Common.ServiceInterface;
using ICP.Business.Common.UI.Contact;
using ICP.Common.UI.Controls;
using ICP.DataCache.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.OceanImport.ServiceInterface;
using ICP.Framework.ClientComponents;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Helper;
using ICP.MailCenter.ServiceInterface;
using ICP.Message.ServiceInterface;
using ICP.Operation.Common.ServiceInterface;
using Microsoft.Office.Interop.Outlook;
using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Application = System.Windows.Forms.Application;
using ContactType = ICP.Framework.CommonLibrary.Common.ContactType;
using EnumDocumentType = ICP.FileSystem.ServiceInterface.DocumentType;
using Exception = System.Exception;
using MethodInvoker = System.Windows.Forms.MethodInvoker;

namespace ICP.Business.Common.UI.Document
{
    /// <summary>
    /// 上传SI附档界面
    /// </summary>
    public partial class frmUploadSIAttachment : BaseEditPart, IDisposable
    {
        #region Fields & Property & Service & Delegate
        /// <summary>
        /// 记录LoadingForm的ID
        /// </summary>
        private int tokenID = -1;
        /// <summary>
        /// 是否正在保存
        /// </summary>
        private bool isSaving = false;
        /// <summary>
        /// 是否设置皮肤
        /// </summary>
        private bool isSetSkin = false;
        /// <summary>
        /// 默认文档类型
        /// </summary>
        private string strAttachmentType = LocalData.IsEnglish ? "--Attachment Type--" : "--附件类型--";
        /// <summary>
        /// 联系人列表绑定过数据
        /// </summary>
        private bool isContactBindData = false;
        /// <summary>
        /// 联系人类型
        /// </summary>
        private ContactType ContactType = ContactType.Customer;
        /// <summary>
        /// WorkItem
        /// </summary>
        public WorkItem WorkItem
        {
            get
            {
                return ServiceClient.GetClientService<WorkItem>();
            }
        }
        
        /// <summary>
        /// 文档列表
        /// </summary>
        private UCBusinessDocumentList ucBusinessDocumentList;
        /// <summary>
        /// 文档列表
        /// </summary>
        public UCBusinessDocumentList UCBusinessDocumentList
        {
            get
            {
                if (ucBusinessDocumentList != null)
                {
                    return ucBusinessDocumentList;
                }
                ucBusinessDocumentList = WorkItem.Items.AddNew<UCBusinessDocumentList>();
                return ucBusinessDocumentList;
            }
        }
        /// <summary>
        /// 联系人列表工具栏
        /// </summary>
        private UCCustomerToolbar _ucCustomerToolbar;
        /// <summary>
        /// 联系人列表工具栏
        /// </summary>
        public UCCustomerToolbar UCCustomerToolbar
        {
            get
            {
                if (_ucCustomerToolbar != null)
                {
                    return _ucCustomerToolbar;
                }
                _ucCustomerToolbar = WorkItem.Items.AddNew<UCCustomerToolbar>();
                return _ucCustomerToolbar;
            }
        }
        /// <summary>
        /// 联系人列表
        /// </summary>
        private UCCustomerList _customerList;
        /// <summary>
        /// 联系人列表
        /// </summary>
        public UCCustomerList CustomerList
        {
            get
            {
                if (_customerList == null)
                {
                    _customerList = WorkItem.Items.AddNew<UCCustomerList>();
                }
                return _customerList;
            }
        }

        /// <summary>
        /// 程序集名称
        /// </summary>
        private string AssamblyName
        {
            get
            {
                MethodBase methodother = MethodBase.GetCurrentMethod();
                if (methodother.DeclaringType != null)
                    return methodother.DeclaringType.FullName;
                return "ICP.Business.Common.UI";
            }
        } 
        /// <summary>
        /// 上传附件参数
        /// </summary>
        private UploadAttachmentParameter Parameter { get; set; }
        /// <summary>
        /// 执行操作类型
        /// </summary>
        private List<CustomEnumInfo> _actionTypes = null;
        /// <summary>
        /// 执行操作类型
        /// </summary>
        public List<CustomEnumInfo> actionTypes
        {
            get
            {
                if (_actionTypes == null)
                {
                    _actionTypes = GetCustomEnumList(typeof(SelectionType), false, Parameter.OperationContext.OperationType);
                }
                return _actionTypes;
            }
            set { _actionTypes = value; }
        }
        /// <summary>
        /// 业务操作上下文
        /// </summary>
        private BusinessOperationContext OperationContext
        {
            get { return Parameter.OperationContext; }
            set { Parameter.OperationContext = value; }
        }
        /// <summary>
        /// 关联信息实体
        /// </summary>
        private List<OperationMessageRelation> operationMessageRelations { get; set; }
        /// <summary>
        /// 消息实体
        /// </summary>
        private Message.ServiceInterface.Message MessageInfo
        {
            get { return Parameter.MessageInfo; }
            set { Parameter.MessageInfo = value; }
        }
        /// <summary>
        /// 联系人列表实体
        /// </summary>
        public ContactObjects contactList
        {
            get
            {
                return FCMCommonService.GetContactList(Parameter.OperationContext.OperationID, Parameter.OperationContext.OperationType);
            }
        }

        /// <summary>
        /// 文档类型集合
        /// </summary>
        private List<CustomEnumInfo> _documentTypes = null;
        /// <summary>
        /// 文档类型集合
        /// </summary>
        public List<CustomEnumInfo> DocumentTypes
        {
            get
            {
                if (_documentTypes == null)
                {
                    _documentTypes = GetCustomEnumList(typeof(EnumDocumentType), false, Parameter.OperationContext.OperationType);
                }
                return _documentTypes;
            }
            set { _documentTypes = value; }
        }
        /// <summary>
        /// 海出服务
        /// </summary>
        public IOceanExportService OceanExportService
        {
            get
            {
                return ServiceClient.GetService<IOceanExportService>();

            }
        }
        /// <summary>
        /// 海进服务
        /// </summary>
        public IOceanImportService OceanImportService
        {
            get
            {
                return ServiceClient.GetService<IOceanImportService>();

            }
        }
        /// <summary>
        /// ICP Common服务
        /// </summary>
        public IICPCommonOperationService CommonOperationService
        {
            get
            {
                return ServiceClient.GetService<IICPCommonOperationService>();
            }
        }
        /// <summary>
        /// FCM Common 服务
        /// </summary>
        public IFCMCommonService FCMCommonService
        {
            get { return ServiceClient.GetService<IFCMCommonService>(); }
        }
        /// <summary>
        /// 搜索器客户端服务
        /// </summary>
        public IDataFindClientService DataFindClientService
        {
            get
            {
                return ServiceClient.GetClientService<IDataFindClientService>();
            }
        }
        /// <summary>
        /// 客户端业务操作服务
        /// </summary>
        public IClientBusinessOperationService ClientBusinessOperationService
        {
            get { return ServiceClient.GetClientService<IClientBusinessOperationService>(); }
        }


        private List<CustomEnumInfo> GetCustomEnumList(Type type, bool isToolBar, OperationType operationType)
        {
            return EnumGetter.Current[type, isToolBar, operationType];
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        private delegate void BindDataDelegate();
        #endregion

        #region 构造函数 & Override & Disposed
        /// <summary>
        /// 构造函数
        /// </summary>
        public frmUploadSIAttachment()
        {
            InitializeComponent();

            if (!LocalData.IsDesignMode)
            {
                if (LocalData.IsEnglish)
                {
                    Locale();
                }
                Load += (sender, e) =>
                {
                    UCBusinessDocumentList.Margin = new Padding(15, 15, 15, 3);
                    UCBusinessDocumentList.Dock = DockStyle.Fill;
                    pnlAttachment.Controls.Add(UCBusinessDocumentList);
                    UCBusinessDocumentList.UploadStartEventHandler += OnUploadStart;
                    UCBusinessDocumentList.UploadFailedEventHandler += OnUploadFailed;
                    UCBusinessDocumentList.UploadSuccessEventHandler += OnUploadSuccess;
                    UCBusinessDocumentList.DocumentExistsEventHandler += OnDocumentExists;
                };
                Disposed += delegate
                    {
                        DisposedCompoment();
                    };
            }
        }

        public override void Init(IDictionary<string, object> values)
        {
            if (values == null) return;
            foreach (var item in values)
            {
                if (item.Key.ToUpper() == "addDocList".ToUpper())
                {
                    List<string> doclist = item.Value as List<string>;
                    UCBusinessDocumentList.AddDocuments(doclist, null, true, null);
                    break;
                }
            }
        }
        /// <summary>
        /// Disposed
        /// </summary>
        private void DisposedCompoment()
        {
            if (ucBusinessDocumentList != null)
            {
                pnlAttachment.Controls.Clear();
                if (WorkItem != null)
                {
                    WorkItem.Items.Remove(ucBusinessDocumentList);
                    WorkItem.Items.Remove(this);
                }

                ucBusinessDocumentList.UploadFailedEventHandler -= OnUploadFailed;
                ucBusinessDocumentList.UploadStartEventHandler -= OnUploadStart;
                ucBusinessDocumentList.UploadSuccessEventHandler -= OnUploadSuccess;
                ucBusinessDocumentList.DocumentExistsEventHandler -= OnDocumentExists;
                cmbDocumentType.OnFirstTimeEnter -= OnDocumentTypeFirstTimeEnter;
                cmbDocumentType.EditValueChanged -= OnDocumentTypeEditValueChanged;
                cmbActionType.OnFirstTimeEnter -= OnAtctionTypeFistTimeEnter;
                cmbActionType.EditValueChanged -= OnActionTypeEditValueChanged;
                ucBusinessDocumentList.Dispose();
                ucBusinessDocumentList = null;
            }
            if (UCCustomerToolbar != null)
            {
                UnRegisterContactToolBarEvents();
            }
            OperationContext = null;
            Parameter = null;
            DocumentTypes = null;
        }

        void IDisposable.Dispose()
        {
        }
        #endregion

        #region Windows Event
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                WaitCallback callback = (data) => BeginInvoke((MethodInvoker)delegate
                {
                    Application.DoEvents();
                    try
                    {
                        Saved();
                    }
                    catch (Exception ex)
                    {
                        LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                    }
                });
                ThreadPool.QueueUserWorkItem(callback);
            }
        }
        /// <summary>
        /// 联系人保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnContactSaved(object sender, EventArgs e)
        {
            using (new CursorHelper(Cursors.WaitCursor))
            {
                if (!CustomerList.IsNullOrEmtpyDataSource())
                {
                    if (CustomerList.ValidateData())
                        CustomerList.InnerSaved(ContactType);
                }
            }
        }
        /// <summary>
        /// 只读改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ReadOnlyChanged(object sender, CommonEventArgs<bool> e)
        {
            bool readOnly = e.Data;
        }
        /// <summary>
        /// 联系人类型改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnContactTypeChanged(object sender, CommonEventArgs<ContactType> e)
        {
            ContactType = e.Data;
            CustomerList.ChangeDataRecordContactType(ContactType);
        }
        /// <summary>
        /// 联系人删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnContactDeleted(object sender, EventArgs e)
        {
            CustomerList.Remove();
        }
        /// <summary>
        /// 联系人添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnContactAdded(object sender, EventArgs e)
        {
            CustomerList.AddNewDataRecord(OperationContext.OperationID, string.Empty, string.Empty);
        }
        /// <summary>
        /// 执行操作类型首次获得焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>1.添加值改变事件 2.绑定数据</remarks>
        private void OnAtctionTypeFistTimeEnter(object sender, EventArgs e)
        {
            cmbActionType.EditValueChanged += OnActionTypeEditValueChanged;
            SetComboxTreeDataSource(cmbActionType, actionTypes);
        }
        /// <summary>
        /// 文档类型首次获得焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>1.添加值改变事件 2.绑定数据</remarks>
        private void OnDocumentTypeFirstTimeEnter(object sender, EventArgs e)
        {
            cmbDocumentType.EditValueChanged += OnDocumentTypeEditValueChanged;
            SetComboxTreeDataSource(cmbDocumentType, DocumentTypes);
        }
        /// <summary>
        /// 执行操作类型值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnActionTypeEditValueChanged(object sender, EventArgs e)
        {
            SelectionType? selectionType = ConvertSelectedActionType();
            if (selectionType.HasValue)
            {
                if (selectionType == SelectionType.Normal)
                {
                    cmbDocumentType.EditValue = strAttachmentType;
                    cmbDocumentType.Enabled = true;
                    SetCheckedContactType(selectionType.Value);
                }
                else
                {
                    //将附件类型带上默认值，并且将控件不能编辑，设置联系人面板Customer/Carrier选项
                    EnumDocumentType? documentType = GetDocumentType(selectionType.Value);
                    UCBusinessDocumentList.UpdateDataListDocumentType(documentType, true);
                    cmbDocumentType.EditValue = EnumHelper.GetDescription(documentType, LocalData.IsEnglish);
                    cmbDocumentType.Enabled = false;
                    SetCheckedContactType(selectionType.Value);
                }
            }
        }
        /// <summary>
        /// 文档类型值改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDocumentTypeEditValueChanged(object sender, EventArgs e)
        {
            EnumDocumentType? documentType = GetDocumentType();
            if (!documentType.HasValue)
            {
                cmbDocumentType.SelectedValue = cmbDocumentType.EditValue = string.Empty;
            }
            UCBusinessDocumentList.UpdateDataListDocumentType(documentType, true);
        }
        /// <summary>
        /// 文档数据绑定完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDocumentDataBindComplete(object sender, EventArgs e)
        {
            //默认把选择邮件的附件添加
            if (Parameter.UploadWay == UploadWay.DirectOpen)
                AddMailAttachments();
            //是否存在拖拽的文件
            //HandleDragDropFile();
        }
        /// <summary>
        /// 文档存在 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDocumentExists(object sender, CommonEventArgs<string> e)
        {
            // newOverrideFileName = e.Data;
            //try
            //{
            //    ShowMessagebox("Tip", e.Data, MessageBoxIcon.Information);
            //}
            //finally
            //{
            //    UnCheckcbxEmailAsAttachment();
            //}
        }
        /// <summary>
        /// 上传开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnUploadStart(object sender, EventArgs e)
        {
            isSaving = true;
            bool isAllowSave = UCBusinessDocumentList.IsAllowSaveData = ValidateData(false);
            if (isAllowSave)
            {
                SetButtonEnablity(false);
                ShowMesageTip((LocalData.IsEnglish ? "Saving is in progress..." : "正在保存。。。"));
            }
        }
        /// <summary>
        /// 上传失败
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnUploadFailed(object sender, CommonEventArgs<string> e)
        {
            SetButtonEnablity(true);
            isSaving = false;

            HideTip();
            SetSkin();
            MessageBoxService.ShowError(e.Data);
        }
        /// <summary>
        /// 上传成功
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnUploadSuccess(object sender, EventArgs e)
        {
            SetButtonEnablity(true);
            PostInfo postInfo = sender as PostInfo;
            if (postInfo != null && postInfo.Saved[0])
            {
                UCBusinessDocumentList.UpgradeDirtyDataSource();
                isSaving = false;
                //HideTip();
                ShowMesageTip((LocalData.IsEnglish ? "Save Successfully." : "保存成功！"));
                //改变事件代码SIR设置为3
                try
                {
                    MailCenterParameter.MailCenterRefresh = true;
                    UpgradeEventCode(postInfo.DocumentType);
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                }

                if (postInfo.Saved[1])
                {
                    if (LocalData.ApplicationType == ApplicationType.EmailCenter)
                    {
                        StopwatchHelper.EndStopwatch(postInfo.StopWatch, DateTime.Now, "ICPMailCenter.exe", "UPLOAD-ATTACHMENT", string.Format((LocalData.IsEnglish ? "MailCenter Upload Attachment;Operation No[{0}]" : "邮件中心上传附件;OperationID[{0}]"), OperationContext.OperationID));
                    }
                    StopwatchHelper.CustomUpdateStopwatchLog(postInfo.StopWatch, postInfo.OperationLogID, false, string.Empty,string.Empty, "上传附件完成");
                    Thread.Sleep(800);
                    Closed();
                }
            }
        }
        /// <summary>
        /// 覆盖重复的文件名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkOverride_EditValueChanged(object sender, EventArgs e)
        {
            UCBusinessDocumentList.CheckedOverride = chkOverride.Checked;
        }
        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            Closed();
        }
        /// <summary>
        /// 界面按键事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmUploadSIAttachment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Closed();
            }
        }
        #endregion

        private void SetSkin()
        {
            if (isSetSkin)
                return;
            OfficeSkins.Register();

            if (!SkinManager.AllowFormSkins)
                SkinManager.EnableFormSkins();

            string skinName = ClientConfig.Current.GetValue("MainFormSkinName");
            if (skinName == null)
            {

                skinName = "Blue";
            }
            UserLookAndFeel.Default.SetSkinStyle(skinName);
            isSetSkin = true;
        }

        public void Init(SelectionType selectionType)
        {
            if (!LocalData.IsEnglish)
            {
                btnSave.Text = "保存";
                btnClose.Text = "关闭";
                cbxEmailAsAttachment.Text = "将邮件作为一个附件";
                chkOverride.Text = "覆盖重复文件名";
            }

            chkOverride.ToolTip = LocalData.IsEnglish ? "Checked if the new attachment(s) will override the existing items which are duplicated. Unchecked if auto-rename the new attachment(s)."
                : "勾选表示如果上传的附件与服务器上存在的文档名重复，则以新文件覆盖旧文件。不勾选表示上传时会自动重命名新文件";
            if (selectionType != SelectionType.Normal)
            {
                cmbDocumentType.Enabled = false;
                EnumDocumentType? defaultDocumentType = GetDocumentType(selectionType);
                cmbDocumentType.EditValue = EnumHelper.GetDescription(defaultDocumentType, LocalData.IsEnglish);
                UCBusinessDocumentList.currentSelectDocumentType = defaultDocumentType;
            }
            else
            {
                cmbDocumentType.Enabled = true;
                cmbDocumentType.EditValue = LocalData.IsEnglish ? "--Attachment Type--" : "--附件类型--";
            }
            cmbActionType.EditValue = EnumHelper.GetDescription(selectionType, LocalData.IsEnglish);

            Controldisplay(!IsNullMailItem());
            cmbActionType.OnFirstTimeEnter -= OnAtctionTypeFistTimeEnter;
            cmbActionType.OnFirstTimeEnter += OnAtctionTypeFistTimeEnter;
            cmbDocumentType.OnFirstTimeEnter -= OnDocumentTypeFirstTimeEnter;
            cmbDocumentType.OnFirstTimeEnter += OnDocumentTypeFirstTimeEnter;
        }

        private void AddCustomerControls(SelectionType selectionType)
        {
            if (selectionType == SelectionType.Normal)
            {
                Size = new Size(719, 310);
                pnlTop.Dock = DockStyle.Fill;
                pnlCustomer.Visible = false;
            }
            else
            {
                Size = new Size(719, 600);
                pnlTop.Dock = DockStyle.Top;
                pnlCustomer.Visible = true;
                if (pnlCustomerToolBar.Controls.Count == 0)
                {
                    UCCustomerToolbar.Dock = DockStyle.Fill;
                    ContactObjects list = FCMCommonService.GetContactList(OperationContext.OperationID, OperationContext.OperationType);
                    if (list != null && list.CustomerCarrier != null)
                    {
                        List<CustomerCarrierObjects> customerList = list.CustomerCarrier;
                        if (customerList != null && customerList.Count > 0)
                        {
                            customerList.ForEach(item =>
                             {
                                 if (!item.IsNew)
                                 {
                                     ContactType = item.Type;
                                 }
                             });

                        }
                    }

                    SetContactType();
                    pnlCustomerToolBar.Controls.Add(UCCustomerToolbar);
                    RegisterContactToolBarEvents();
                }

                if (pnlCustomerList.Controls.Count == 0)
                {
                    CustomerList.Dock = DockStyle.Fill;
                    pnlCustomerList.Controls.Add(CustomerList);
                    CustomerList.OperationContext = Parameter.OperationContext;
                    CustomerList.BringToFront();
                }
            }
        }

        private void SetContactType()
        {
            UCCustomerToolbar.Init(ContactType, false);
        }

        private void RegisterContactToolBarEvents()
        {
            _ucCustomerToolbar.Added += OnContactAdded;
            _ucCustomerToolbar.Deleted += OnContactDeleted;
            _ucCustomerToolbar.Saved += OnContactSaved;
            _ucCustomerToolbar.ContactTypeChanged += OnContactTypeChanged;
            _ucCustomerToolbar.ReadOnlyChanged += ReadOnlyChanged;
        }

        private void UnRegisterContactToolBarEvents()
        {
            UCCustomerToolbar.Added -= OnContactAdded;
            UCCustomerToolbar.Deleted -= OnContactDeleted;
            UCCustomerToolbar.Saved -= OnContactSaved;
            UCCustomerToolbar.ContactTypeChanged -= OnContactTypeChanged;
            UCCustomerToolbar.ReadOnlyChanged -= ReadOnlyChanged;
        }
        private CustomEnumInfo GetSelectedRow(SelectedType selectedType, object selectedValue)
        {
            if (selectedValue != null && !string.IsNullOrEmpty(selectedValue.ToString()))
            {
                Guid id = Guid.Empty;
                if (selectedType == SelectedType.DocumentType)
                {
                    id = new Guid(selectedValue.ToString());
                    return DocumentTypes.Find(item => item.ID == id);
                }
                else
                {
                    id = new Guid(selectedValue.ToString());
                    return actionTypes.Find(item => item.ID == id);
                }
            }
            return null;
        }
        /// <summary>
        /// 文档类型构造树形结构
        /// </summary>
        /// <param name="cmbTree"></param>
        /// <param name="dataList"></param>
        private void SetComboxTreeDataSource(LWComboBoxTree cmbTree, List<CustomEnumInfo> dataList)
        {
            cmbTree.RootValue = Guid.Empty;
            cmbTree.ParentMember = "ParentID";
            cmbTree.ValueMember = "ID";
            cmbTree.DisplayMember = LocalData.IsEnglish ? "Etip" : "Ctip";
            cmbTree.DataSource = dataList;
            cmbTree.SetComboBoxTreeState(true);
            cmbTree.AllowMultSelect = false;
            cmbTree.InitSelectedNode(null);
        }
        
        /// <summary>
        /// 转换执行操作类型(ActionType)到工作类型(SelectionType)
        /// </summary>
        /// <returns></returns>
        private SelectionType? ConvertSelectedActionType()
        {
            SelectionType? selectionType = null;
            CustomEnumInfo info = GetSelectedRow(SelectedType.ActionType, cmbActionType.SelectedValue);
            if (info != null)
            {
                selectionType = (SelectionType)Enum.Parse(typeof(SelectionType), info.Tag);
            }
            else
            {
                selectionType = Parameter.SelectionType;
            }
            return selectionType;
        }

        /// <summary>
        /// 设置联系人面板选择项
        /// </summary>
        private void SetCheckedContactType(SelectionType selectionType)
        {
            switch (selectionType)
            {
                case SelectionType.SI:
                    Height = 600;
                    pnlCustomer.Visible = true;
                    AddCustomerControls(selectionType);
                    SetContactType();
                    if (!isContactBindData)
                    {
                        ContactListBindData(Parameter.MessageInfo);
                    }
                    break;
                case SelectionType.SO:
                case SelectionType.AP:
                case SelectionType.AN:
                case SelectionType.MBL:
                    Height = 600;
                    pnlCustomer.Visible = true;
                    AddCustomerControls(selectionType);
                    SetContactType();
                    if (!isContactBindData)
                    {
                        ContactListBindData(Parameter.MessageInfo);
                    }
                    break;
                case SelectionType.Normal:
                    Height = 310;
                    pnlCustomer.Visible = false;
                    break;
                default:
                    Height = 310;
                    pnlCustomer.Visible = false;
                    break;
            }
        }

        private EnumDocumentType? GetDocumentType(SelectionType selectionType)
        {
            switch (selectionType)
            {
                case SelectionType.SI:
                    return EnumDocumentType.SI;
                case SelectionType.SO:
                    return EnumDocumentType.OSO;
                case SelectionType.MBL:
                    return EnumDocumentType.MBL;
                case SelectionType.AP:
                    return EnumDocumentType.AP;
                case SelectionType.AN:
                    return EnumDocumentType.AN;
                case SelectionType.Normal:
                    return null;
                default:
                    return EnumDocumentType.Other;
            }
        }


        /// <summary>
        /// Combox框选择的DocumentType
        /// </summary>
        /// <returns></returns>
        private EnumDocumentType? GetDocumentType()
        {
            EnumDocumentType? documentType = null;
            CustomEnumInfo info = GetSelectedRow(SelectedType.DocumentType, cmbDocumentType.SelectedValue);
            if (info != null)
            {
                if (!info.HasChildNodes)
                {
                    documentType = (EnumDocumentType)Enum.Parse(typeof(EnumDocumentType), info.eCaption);
                }
            }
            return documentType;
        }

        /// <summary>
        /// 如果Combox框没有选择DocumentType，就获取邮件菜单上传附件的DocumentType
        /// </summary>
        /// <returns></returns>
        private IEnumerable<EnumDocumentType?> GetDocumentTypes()
        {
            return (from item in ucBusinessDocumentList.DataSource
                    where item.IsDirty && (
                          item.DocumentType == EnumDocumentType.SI || item.DocumentType == EnumDocumentType.OSO ||
                          item.DocumentType == EnumDocumentType.AN || item.DocumentType == EnumDocumentType.AP ||
                          item.DocumentType == EnumDocumentType.MBL)
                    select item.DocumentType).GroupBy(s => s.Value).Select(s => s.First()).ToList();
        }
        

        
        private bool IsNullMailItem()
        {
            if (Parameter.MailItem == null || Parameter.MailItem == "")
                return true;
            else
                return false;
        }



        private void AddMailAttachments()
        {
            List<string> attachmentFilePaths = null; List<string> previewPaths = null;
            bool result = GetMailAttachmentPaths(out attachmentFilePaths, out previewPaths);
            if (!result)
            {
                // this.pnlDocumentType.Visible = false;
                return;
            }

            EnumDocumentType? documentType = GetDocumentType(Parameter.SelectionType);
            UCBusinessDocumentList.AddDocuments(attachmentFilePaths, previewPaths, chkOverride.Checked, documentType);
        }


        private bool GetMailAttachmentPaths(out List<string> attachmentFilePaths, out List<string> previewPaths)
        {
            attachmentFilePaths = previewPaths = null;
            if (Parameter.MessageInfo == null)
                return false;
            var attachments = Parameter.MessageInfo.Attachments;
            if (attachments == null)
                return false;
            int count = attachments.Count;
            if (count <= 0)
                return false;
            attachmentFilePaths = new List<string>();
            previewPaths = new List<string>();
            for (int i = 0; i < count; i++)
            {
                AttachmentContent item = attachments[i];
                attachmentFilePaths.Add(item.ClientPath);
                previewPaths.Add(item.PreviewPath);
            }

            if (attachmentFilePaths == null || attachmentFilePaths.Count <= 0)
                return false;
            return true;
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="uploadAttachmentParameter">上传附件参数类</param>
        public void BindingData(object uploadAttachmentParameter)
        {
            UploadAttachmentParameter par = uploadAttachmentParameter as UploadAttachmentParameter;
            Parameter = par;
            Init(Parameter.SelectionType);
            //if (parameter.OperationType == OperationType.OceanExport)
            //{
            Parameter.GetOperationContext();
            AddCustomerControls(Parameter.SelectionType);
            //}
            Text = string.Format("{0} - {1}", (LocalData.IsEnglish ? "Upload Attachment" : "上传附件"), OperationContext.OperationNO);
            UCBusinessDocumentList.DataBindCompleteEventHandler += OnDocumentDataBindComplete;
            UCBusinessDocumentList.BindData(OperationContext);

            if (Parameter.SelectionType != SelectionType.Normal)
            {
                ContactListBindData(par.MessageInfo);
            }
        }

        public override object DataSource
        {
            set { BindingData(value); }
        }

        private void ContactListBindData(Message.ServiceInterface.Message messageInfo)
        {
            List<CustomerCarrierObjects> customerList =
                   FCMInterfaceUtility.GetOperationContactsAndMailContacts(messageInfo, OperationContext, false, true, false); ;
            CustomerList.InsertList(customerList);
            customerList.Clear();
            customerList = null;
            isContactBindData = true;
        }
        

        private void UpgradeEventCode(EnumDocumentType? documentType)
        {
            if (documentType.HasValue)
                switch (documentType.Value)
                {
                    case EnumDocumentType.SI:
                        InnerSaved(EnumHelper.GetDescription(EnumDocumentType.SI), 3);
                        break;
                    case EnumDocumentType.OSO:
                        InnerSaved(EnumHelper.GetDescription(EnumDocumentType.OSO), 1);
                        break;
                    case EnumDocumentType.MBL:
                        InnerSaved(EnumHelper.GetDescription(EnumDocumentType.MBL), 1);
                        break;
                    case EnumDocumentType.AN:
                        InnerSaved(EnumHelper.GetDescription(EnumDocumentType.AN), 1);
                        break;
                    case EnumDocumentType.AP:
                        InnerSaved(EnumHelper.GetDescription(EnumDocumentType.AP), 1);
                        break;
                    case EnumDocumentType.AN_C:
                        InnerSaved(EnumHelper.GetDescription(EnumDocumentType.AN_C), 1);
                        break;
                }
        }

        private bool InnerSaved(string key, object value)
        {
            CommonOperationService.Save(GetBusinessParameters(key, value), CreateBusinessOperationParameterInfo());
            return true;
        }

        private List<BusinessSaveParameter> GetBusinessParameters(string key, object value)
        {
            BusinessSaveParameter saveParameter = new BusinessSaveParameter();
            saveParameter["OceanBookingID"] = OperationContext.OperationID;
            saveParameter[key] = value;
            return new List<BusinessSaveParameter>(1) { saveParameter };
        }

        private BusinessOperationParameter CreateBusinessOperationParameterInfo()
        {
            return new BusinessOperationParameter()
             {
                 TemplateCode = Parameter.TemplateCode,
                 Context = OperationContext
             };
        }

        private void SetButtonEnablity(bool enable)
        {
            btnClose.Enabled = btnSave.Enabled = enable;
        }

        /// <summary>
        /// 本地化
        /// </summary>
        private void Locale()
        {
        }
        /// <summary>
        /// 显示提示消息
        /// </summary>
        /// <param name="message"></param>
        private void ShowMesageTip(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                lblSaveTip.Visible = true;
                lblSaveTip.ForeColor = Color.Blue;
                lblSaveTip.Text = message;
            }
            else
            {
                lblSaveTip.Visible = false;
            }
        }

        private void HideTip()
        {
            lblSaveTip.Visible = false;
        }

        /// <summary>
        /// 验证数据的是否有效
        /// </summary>
        /// <returns></returns>
        public bool ValidateData(bool showMessage)
        {
            bool validateDocumentList = UCBusinessDocumentList.ValidateDate(showMessage);

            return validateDocumentList;
        }
        /// <summary>
        /// 保存
        /// </summary>
        private void Saved()
        {
            if (!ValidateData(true))
                return;

            bool[] saveSucess = new bool[2];

            //邮件中心记录上传附件的花费时间
            Stopwatch stopwatch = stopwatch = Stopwatch.StartNew();
            Guid OperationLogID = Guid.NewGuid();
            StopwatchHelper.CustomRecordStopwatch(stopwatch, OperationLogID, DateTime.Now, AssamblyName,
                "UPLOAD-ATTACHMENT", string.Format("上传附件;OperationID[{0}]", OperationContext.OperationID));
            try
            {
                EnumDocumentType? documentType = UCBusinessDocumentList.currentSelectDocumentType;
                Form findForm = FindForm();
                if (findForm != null) findForm.TopMost = false;
                //开启动画
                tokenID = LoadingServce.ShowLoadingForm("Loading.....");
                bool isSaved = UCBusinessDocumentList.Save();
                StopwatchHelper.CustomUpdateStopwatchLog(stopwatch, OperationLogID, true, "上传附件完成");
                //关闭动画
                LoadingServce.CloseLoadingForm(tokenID);
                if (findForm != null) findForm.TopMost = true;
                if (isSaved)
                {
                    //selectionType = ConvertSelectedActionType();
                    //SaveMemo(selectionType.Value);
                    IEnumerable<EnumDocumentType?> documentTypes = GetDocumentTypes();
                    foreach (var doc in documentTypes)
                    {
                        SaveMemo(doc.Value);
                    }
                }
                saveSucess[0] = isSaved;
                saveSucess[1] = true;
                //保存文档时，默认会将更改的联系人保存
                if (OperationContext != null && contactList != null)
                {
                    if (pnlCustomer.Visible && CustomerList.IsChanged)
                    {
                        SetSkin();
                        DialogResult result = MessageBox.Show(LocalData.IsEnglish
                                ? "Contact information has changed, Do you want to save?"
                                : "联系人信息有修改，是否保存?", "Tip", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            if (CustomerList.ValidateData())
                                CustomerList.Save();
                            else
                                saveSucess[1] = false;
                        }
                        StopwatchHelper.CustomUpdateStopwatchLog(stopwatch, OperationLogID, true, string.Empty,"联系人信息更新完成");
                    }
                    //this.CustomerList.Save(parameter.OperationContext.OperationID, parameter.OperationContext.OperationType, (DateTime?)parameter.OperationContext.UpdateDate, dataList.CustomerCarrier);
                }
                OnUploadSuccess(new PostInfo() { Saved = saveSucess, DocumentType = documentType,OperationLogID=OperationLogID, StopWatch = stopwatch }, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                isSaving = false;
                //关闭动画
                LoadingServce.CloseLoadingForm(tokenID);
                Logger.Log.Info("上传附件报错:" + Environment.NewLine + ex.ToString());
            }
        }

        private void SaveMemo(EnumDocumentType documentType)
        {
            EventObjects eventObject = null;
            switch (documentType)
            {
                case EnumDocumentType.SI:
                    eventObject=CreateEventInfo(ContactStage.SI, EventCodeList(EnumHelper.GetDescription(EnumDocumentType.SI)));
                    break;
                case EnumDocumentType.OSO:
                    eventObject=CreateEventInfo(ContactStage.SO, EventCodeList(EnumHelper.GetDescription(EnumDocumentType.OSO)));
                    break;
                case EnumDocumentType.MBL:
                    eventObject=CreateEventInfo(ContactStage.SI, EventCodeList(EnumHelper.GetDescription(EnumDocumentType.MBL)));
                    break;
                case EnumDocumentType.AP:
                    eventObject=CreateEventInfo(ContactStage.SI, EventCodeList(EnumHelper.GetDescription(EnumDocumentType.AP)));
                    break;
                case EnumDocumentType.AN:
                    eventObject=CreateEventInfo(ContactStage.AN, EventCodeList(EnumHelper.GetDescription(EnumDocumentType.AN)));
                    break;
                default:
                    //
                    break;
            }
            if (eventObject != null)
                FCMCommonService.SaveMemoInfo(eventObject);
        }

        #region  读取当前CODE的详细的信息
        private List<EventCode> eventCodeList = new List<EventCode>();
        /// <summary>
        /// 返回当前CODE的事件详细信息
        /// </summary>
        /// <returns></returns>
        public EventCode EventCodeList(string code)
        {
            if (eventCodeList.Any() == false)
            {
                eventCodeList = FCMCommonService.GetEventCodeList(Parameter.OperationType);
            }
            return eventCodeList.FirstOrDefault(n => n.Code == code);
        }
        #endregion

        private EventObjects CreateEventInfo(ContactStage stage, EventCode eventCode)
        {
            EventObjects eventObject=null;
            if (eventCode == null) return null;
            eventObject = new EventObjects()
            {
                OperationID = OperationContext.OperationID,
                OperationType = OperationContext.OperationType,
                FormID = OperationContext.FormId,
                FormType = OperationContext.FormType,
                Code = eventCode.Code,
                CreateDate = DateTime.Now,
                Description = eventCode.Subject,
                CategoryName = eventCode.Category,
                IsShowAgent = true,
                IsShowCustomer = true,
                UpdateBy = LocalData.UserInfo.LoginID,
                Subject = eventCode.Subject
            };
            Guid imessageID = Guid.Empty;
            operationMessageRelations = WorkItem.State[Operation.Common.ServiceInterface.Constants.CurrentMessageRelationKey] as List<OperationMessageRelation>;
            if (operationMessageRelations != null && operationMessageRelations.Count > 0)
            {
                if (Parameter.MessageInfo.MessageId == operationMessageRelations[0].MessageId)
                {
                    imessageID = operationMessageRelations[0].IMessageId;
                    if (!operationMessageRelations.Any(item => item.OperationID == OperationContext.OperationID))
                    {
                        FetchMailMessageID(imessageID, stage);
                    }
                }
                else
                {
                    imessageID = FetchMailMessageID(Guid.Empty, stage);
                }
            }
            else
            {
                imessageID = FetchMailMessageID(Guid.Empty, stage);
            }
            eventObject.MessageID = imessageID;
            eventObject.Type = MemoType.EmailLog;

            return eventObject;
        }

        /// <summary>
        /// 如果有没保存，先保存邮件，在保存关联信息
        /// </summary>
        /// <param name="iMessageid"></param>
        /// <param name="stage"></param>
        /// <returns></returns>
        private Guid FetchMailMessageID(Guid iMessageid, ContactStage stage)
        {
            Guid messageID = Guid.Empty;

            //将邮件保存，以及邮件关联信息保存，并且标识为SI阶段
            MessageUserPropertiesObject userProperties = new MessageUserPropertiesObject();
            userProperties.OperationId = OperationContext.OperationID;
            userProperties.OperationType = OperationContext.OperationType;
            userProperties.FormId = OperationContext.FormId;
            userProperties.FormType = OperationContext.FormType;
            userProperties.ContactStage = stage.ToString();
            MessageInfo.Type = MessageType.Email;
            MessageInfo.State = MessageState.Success;
            MessageInfo.UserProperties = userProperties;

            OperationMessageRelation messageRelation = FCMInterfaceUtility.CreateOperationMessageRelationInfo(
                Guid.NewGuid(), null, OperationContext.OperationID, OperationContext.OperationType, MessageInfo.MessageId, iMessageid, stage);

            operationMessageRelations = CommonOperationService.EnsureAndSaveMailMessageReference(messageRelation, MessageInfo);
            if (operationMessageRelations != null && operationMessageRelations.Count > 0)
            {
                messageID = operationMessageRelations[0].IMessageId;
            }

            WorkItem.State[Operation.Common.ServiceInterface.Constants.CurrentMessageRelationKey] = operationMessageRelations;
            return messageID;
        }

        private string GetMailSubject()
        {
            string subject = MessageInfo == null ? "Temp" : MessageInfo.Subject;

            subject = subject.Trim();
            char[] invalidChars = Path.GetInvalidFileNameChars();
            foreach (char invalidChar in invalidChars)
            {
                subject = subject.Replace(invalidChar.ToString(), "");
            }
            return subject;

        }
        /// <summary>
        /// 邮件转换成附件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxEmailAsAttachment_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = cbxEmailAsAttachment.Checked;
            String mailFileName = String.Format("{0}.msg", GetMailSubject());
            if (isChecked)
            {
                string savePath = SaveMailAsFile(mailFileName);
                string previewPath = SaveMailAsFile(mailFileName);
                //RemoveMailAttachments();
                EnumDocumentType? documentType = null;
                string normalSelectionType = EnumHelper.GetDescription(SelectionType.Normal, LocalData.IsEnglish);
                if (cmbActionType.EditValue.ToString() == normalSelectionType)
                {
                    documentType = GetDocumentType();
                }
                else
                {
                    SelectionType? selectionType = ConvertSelectedActionType();
                    if (selectionType.HasValue && selectionType.Value != SelectionType.Normal)
                        documentType = GetDocumentType(selectionType.Value);
                }

                UCBusinessDocumentList.AddDocuments(new List<string>(1) { savePath },
                                                         new List<string>(1) { previewPath }, chkOverride.Checked,
                                                         documentType);
            }
            else
            {
                // if (!string.IsNullOrEmpty(newOverrideFileName))
                //    this.UCBusinessDocumentList.Delete(new List<string>(1) { newOverrideFileName });
                //else
                UCBusinessDocumentList.Delete(new List<string>(1) { mailFileName });
            }
        }

        private string SaveMailAsFile(string mailFileName)
        {
            string basePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }
            string savePath = Path.Combine(basePath, mailFileName);
            if (!File.Exists(savePath))
            {
                if (!string.IsNullOrEmpty(Parameter.MessageInfo.EntryID))
                {
                    Microsoft.Office.Interop.Outlook.Application myOutlookApp = new Microsoft.Office.Interop.Outlook.Application();
                    NameSpace myNameSpace = myOutlookApp.GetNamespace("MAPI");
                    MailItem mailItem = myNameSpace.Session.GetItemFromID(Parameter.MessageInfo.EntryID, Type.Missing) as MailItem;
                    Parameter.MailItem = mailItem;
                }
                MailUtility.SaveMailItemAs(Parameter.MailItem, savePath);
            }

            return savePath;
        }

        /// <summary>
        /// 控制控件是否显示
        /// </summary>
        /// <param name="flg">true为显示，false为不显示</param>
        public void Controldisplay(bool flg)
        {
            cbxEmailAsAttachment.Visible = flg;
        }
        

        /// <summary>
        /// 关闭窗口时，如果文件未保存或联系人未保存，需要提示用户是否保存。
        /// </summary>
        private void Closed()
        {
            //SetSkin();
            //DialogResult dialogResult = DialogResult.None;
            //if (UCBusinessDocumentList.IsChanged)
            //    dialogResult = Utility.ShowMessageBox("Tip",
            //                                          (LocalData.IsEnglish
            //                                               ? "The files data has changed,Are you sure to save?"
            //                                               : "附档数据有改动，需要保存吗？"), MessageBoxButtons.YesNo,
            //                                          MessageBoxIcon.Question);
            //else
            //{
            //    if (CustomerList.IsChanged)
            //        dialogResult = Utility.ShowMessageBox("Tip",
            //                                              (LocalData.IsEnglish
            //                                                   ? "Contact information has changed, Do you want to save?"
            //                                                   : "联系人信息有修改，是否保存?"),
            //                                              MessageBoxButtons.YesNo,
            //                                              MessageBoxIcon.Question);
            //}

            //if (dialogResult == DialogResult.Yes)
            //{
            //    using (new CursorHelper(Cursors.WaitCursor))
            //    {
            //        Saved();
            //    }
            //}

            Form findForm = FindForm();
            if (findForm != null) findForm.Close();
        }

        

        #region Comment Code
        //private void InnerLoad()
        //{
        //    if (operationContext == null)
        //        return;
        //    WaitCallback callback = (data) =>
        //    {
        //        try
        //        {
        //            if (IsHandleCreated)
        //            {
        //                var bindDelegate = new BindDataDelegate(InnerBindData);
        //                BeginInvoke(bindDelegate, parameter);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            SetSkin();
        //            Utility.ShowMessageBox("Error", ex.Message, MessageBoxButtons.YesNo, MessageBoxIcon.Error);
        //        }

        //    };
        //    ThreadPool.QueueUserWorkItem(callback);
        //}

        //private void InnerBindData()
        //{
        //    Guid operationId = operationContext.OperationID;
        //    UCBusinessDocumentList.Context = operationContext;
        //    UCBusinessDocumentList.BindData(operationContext);
        //    OceanBookingInfo bookInfo = OceanExportService.GetOceanBookingInfo(operationId);
        //}
        //void ComboxParseEditValue(object sender, ConvertEditValueEventArgs e)
        //{
        //    e.Value = e.Value == null ? string.Empty : e.Value.ToString();
        //    e.Handled = true;
        //}
        //private void RemoveMailAttachments()
        //{
        //    List<string> attachmentFilePaths = null;
        //    List<string> previewPaths = null;
        //    bool result = GetMailAttachmentPaths(out attachmentFilePaths, out previewPaths);
        //    if (!result)
        //        return;
        //    List<string> attachmentFileNames = (from path in attachmentFilePaths
        //                                        select Path.GetFileName(path)).ToList();
        //    UCBusinessDocumentList.Delete(attachmentFileNames);
        //}
        ///// <summary>
        ///// 判断上传附件文档类型
        ///// </summary>
        //private bool JudgeUploadAttachmentDocumentType()
        //{
        //    if (cmbDocumentType.EditValue == null || cmbDocumentType.EditValue.ToString().Equals(strAttachmentType))
        //    {
        //        MessageBox.Show(LocalData.IsEnglish ? "please selected the document type!" : "请选择文档类型!");
        //        isSaving = false;
        //        return false;
        //    }
        //    return true;
        //}
        ///// <summary>
        ///// 如果正在保存，则禁止关闭窗体
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void frmUploadSIAttachment_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    e.Cancel = isSaving;
        //}
        //private void HandleDragDropFile()
        //{
        //    if (parameter.UploadWay == UploadWay.DragDrop)
        //    {
        //        string Command_DragDrop = "DragDropFiles";
        //        var dragDropFiles = WorkItem.State[Command_DragDrop] as List<string>;
        //        if (dragDropFiles != null && dragDropFiles.Count > 0)
        //        {
        //            UCBusinessDocumentList.AddDocuments(dragDropFiles, null, chkOverride.Checked, GetDocumentType(parameter.SelectionType));
        //        }
        //    }
        //}
        ///// <summary>
        ///// 取消将邮件作为附档上传按钮的选中状态
        ///// </summary>
        //private void UnCheckcbxEmailAsAttachment()
        //{
        //    cbxEmailAsAttachment.CheckedChanged -= cbxEmailAsAttachment_CheckedChanged;
        //    cbxEmailAsAttachment.Checked = false;
        //    cbxEmailAsAttachment.CheckedChanged += cbxEmailAsAttachment_CheckedChanged;
        //}
        #endregion

    }
}

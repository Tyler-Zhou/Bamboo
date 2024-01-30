using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ICP.DataCache.ServiceInterface;
using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Server;
using ICP.Operation.Common.ServiceInterface;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Message.ServiceInterface;
using ICP.MailCenter.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.MailCenter.UI;

namespace ICP.Business.Common.UI.Contact
{

    /// <summary>
    /// 联系人列表--职员信息列表
    /// </summary>
    public partial class UCContactStaffListPart : DevExpress.XtraEditors.XtraUserControl//BaseListPart
    {
        #region Service


        public WorkItem Workitem
        {
            get { return ServiceClient.GetClientService<WorkItem>(); }
        }

        public IClientBusinessOperationService ClientBusinessOperationService
        {
            get { return ServiceClient.GetClientService<IClientBusinessOperationService>(); }
        }
        public ICP.FCM.Common.ServiceInterface.IFCMCommonService FCMCommonService
        {
            get { return ServiceClient.GetService<ICP.FCM.Common.ServiceInterface.IFCMCommonService>(); }
        }

        public IOutLookService OutLookService
        {
            get
            {
                ClientHelper.EnsureEmailCenterAppStarted();
                return new OutlookService();
            }
        }

        public IUserService userService
        {
            get { return ServiceClient.GetService<IUserService>(); }
        }

        public IBusinessQueryService BusinessQueryService
        {
            get { return ServiceClient.GetService<IBusinessQueryService>(); }
        }
        public IStaffService staffService
        {
            get { return ServiceClient.GetService<IStaffService>(); }
        }
        #endregion

        #region 属性
        bool LoadDataSouce = false;
        private Guid OperationID;
        private OperationType OperationType;
        private string roleName;
        private DataTable dtList;
        private List<UserList> userList;
        private bool isNewDataSource;
        #endregion

        #region 初始化
        public UCContactStaffListPart()
        {
            InitializeComponent();
            this.Disposed += delegate
            {
                gvStaffList.FocusedRowChanged -= new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(OnGridViewFocusedRowChanged);
                cmbRole.EditValueChanged -= new EventHandler(OnRoleComboBoxEditValueChanged);
                cmbName.EditValueChanged -= new EventHandler(OnNameComboBoxEditValueChanged);
                cmbRole.ParseEditValue -= new DevExpress.XtraEditors.Controls.ConvertEditValueEventHandler(OnComboxParseEditValue);
                this.StaffList = null;
                this.gcStaff.DataSource = null;
                if (bsStaff != null)
                {
                    this.bsStaff.DataSource = null;
                    this.bsStaff = null;
                }
                if (Workitem != null)
                    Workitem.Items.Remove(this);
            };

            if (LocalData.IsEnglish == false) SetCnText();
            this.CreateHandle();
            RegisterEvents();
        }

        private void RegisterEvents()
        {
            gvStaffList.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(OnGridViewFocusedRowChanged);
            cmbRole.EditValueChanged += new EventHandler(OnRoleComboBoxEditValueChanged);
            cmbName.EditValueChanged += new EventHandler(OnNameComboBoxEditValueChanged);
            cmbName.ParseEditValue += new DevExpress.XtraEditors.Controls.ConvertEditValueEventHandler(OnComboxParseEditValue);
        }



        #endregion

        #region 方法
        public StaffObjects CurrentRow
        {
            get { return bsStaff.Current as StaffObjects; }
        }
        /// <summary>
        /// 选中的数据行
        /// </summary>
        public StaffObjects FocusedDataRow
        {
            get { return this.gvStaffList.GetFocusedRow() as StaffObjects; }
        }
        /// <summary>
        /// 当前选中的业务
        /// </summary>
        private DataTable operationInfo { get; set; }

        private void SetCnText()
        {
            grpStaff.Text = LocalData.IsEnglish ? "Assistant List" : "参与者列表";
            colRole.Caption = "角色";
            colRole.ToolTip = "选择角色";
            colName.Caption = "名称";
            colMail.Caption = "邮箱";
            colTel.Caption = "电话";
        }

        /// <summary>
        /// 设置数据源
        /// </summary>
        public object DataSource
        {
            get
            {
                return bsStaff.DataSource;
            }
            set
            {
                BindingData(value);
                //bsStaff.DataSource = value;
                //bsStaff.ResetBindings(false);
            }
        }

        public List<StaffObjects> DataList
        {
            get { return DataSource as List<StaffObjects>; }
        }

        List<StaffObjects> StaffList = null;

        /// <summary>
        /// 员工列表数据源绑定
        /// </summary>
        /// <param name="value"></param>
        private void BindingData(object value)
        {
            StaffList = value as List<StaffObjects>;
            if (StaffList == null)
            {
                if (value.GetType() == typeof(StaffObjects))

                    this.bsStaff.DataSource = value as StaffObjects;
                else
                    this.bsStaff.DataSource = typeof(StaffObjects);
                //this.Enabled = false;
            }
            else
            {
                //this.Enabled = true;
                bsStaff.DataSource = StaffList;
            }
            bsStaff.ResetBindings(false);
        }

        public void Init(bool _LoadDataSource, Guid _operationID, OperationType _operationType)
        {
            this.LoadDataSouce = _LoadDataSource;
            this.OperationID = _operationID;
            this.OperationType = _operationType;
        }

        private ICP.Message.ServiceInterface.Message InitMessage()
        {
            ICP.Message.ServiceInterface.Message message = new ICP.Message.ServiceInterface.Message();
            message.SendFrom = LocalData.UserInfo.EmailAddress;
            message.SendTo = CurrentRow.Mail;
            message.Type = MessageType.Email;
            return message;
        }

        public void NewDataRecord(Guid operationID, OperationType operationType, Guid? formID, FormType? formType)
        {
            this.Insert(CreateStaffInfo(operationID, operationType, formID, formType));
        }
        /// <summary>
        /// 从列表中移除当前项
        /// </summary>
        public void RemoveCurrent()
        {
            if (bsStaff.Current != null)
            {
                bsStaff.RemoveCurrent();
            }
        }


        public void SetDataSource(Guid operationID, OperationType operationType, bool clearDataSource)
        {
            //WaitCallback callback = (obj) => this.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate
            //    {
            if (this.InvokeRequired)
            {
                this.Invoke((System.Windows.Forms.MethodInvoker)delegate
                    {
                        InnerSetDataSource(operationID, operationType, clearDataSource);
                    });
            }
            else
            {
                InnerSetDataSource(operationID, operationType, clearDataSource);
            }
            //   });
            // ThreadPool.QueueUserWorkItem(callback);
        }

        private void InnerSetDataSource(Guid operationID, OperationType operationType, bool clearDataSource)
        {
            if (clearDataSource)
            {
                this.bsStaff.DataSource = null;
            }
            try
            {
                bool isExsits = ExsitsOperationInfo(operationID, operationType);
                if (isExsits)
                {
                    object[] array = operationInfo.Rows[0].ItemArray;
                    //与当前选择的业务没有关联
                    if (!array.Contains(LocalData.UserInfo.LoginID))
                    {

                        Workitem.Commands["Command_AddStaffListPart"].Execute();
                        GetAssistantInfoInServiceDB(operationID, operationType, clearDataSource);
                        SetRoleComboxDataSource();
                    }
                    else
                    {
                        //清空参与者面板
                        Workitem.Commands["Commmand_ClearStaffListPart"].Execute();
                    }
                }
                else
                {
                    GetAssistantInfoInServiceDB(operationID, operationType, clearDataSource);
                    SetRoleComboxDataSource();
                }
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
            }
        }


        /// <summary>
        /// 判断是否存在业务
        /// </summary>
        /// <returns></returns>
        private bool IsExsisOperation()
        {
            if (operationInfo == null || operationInfo.Rows.Count <= 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 本地缓存中查找该票业务，如果不存在该票业务，就从服务端下载到本地
        /// </summary>
        /// <param name="operationID"></param>
        /// <param name="operationType"></param>
        private bool ExsitsOperationInfo(Guid operationID, OperationType operationType)
        {
            DataTable dt = null;
            operationInfo = ClientBusinessOperationService.GetOperationAssistantInfo(operationID, operationType);
            if (dt == null || dt.Rows.Count <= 0)
            {
                operationInfo = BusinessQueryService.GetOperationInfo(new List<Guid>(1) { operationID }, operationType);
                if (operationInfo == null || operationInfo.Rows.Count <= 0)
                    return false;
            }

            return true;
        }

        public void InitControls(Guid operationID)
        {
            SetRoleComboxDataSource();
            SetNameComboxDataSource(operationID);
        }

        public void SetRoleComboxDataSource()
        {
            if (CurrentRow != null)
            {
                if (isNewDataSource || CurrentRow.CanUpdate)
                {
                    if (cmbRole.Items == null || cmbRole.Items.Count == 0)
                    {
                        cmbRole.BeginUpdate();
                        dtList = FCMCommonService.GetOceanFixedRoles();
                        InitRoles(dtList);
                        cmbRole.EndUpdate();
                    }
                }
            }
        }

        public void SetNameComboxDataSource(Guid operationID)
        {
            if (isNewDataSource || CurrentRow.CanUpdate)
            {
                if (this.cmbName.Items == null || this.cmbName.Items.Count == 0)
                {
                    Guid[] arrOrganizationIDs = (from organization in LocalData.UserInfo.UserOrganizationList
                                                 where organization.Type == LocalOrganizationType.Company
                                                 select organization.ID).ToArray();

                    if (IsExsisOperation())
                    {
                        Guid companyID = operationInfo.Rows[0].Field<Guid>("CompanyID");
                        if (!arrOrganizationIDs.Contains(companyID))
                        {
                            Array.Resize(ref arrOrganizationIDs, arrOrganizationIDs.Length + 1);
                            arrOrganizationIDs[arrOrganizationIDs.Length - 1] = companyID;
                        }
                    }

                    userList = userService.GetUnderlingUserList(arrOrganizationIDs.ToArray(), null, null, true);
                    if (userList != null && userList.Count > 0)
                    {
                        List<ListItem> arrItems = new List<ListItem>();
                        Array.ForEach(userList.ToArray(),
                            item => arrItems.Add(new ListItem(item.ID.ToString(), (LocalData.IsEnglish ? item.EName : item.CName))));

                        this.cmbName.BeginUpdate();
                        this.cmbName.Items.AddRange(arrItems);
                        this.cmbName.EndUpdate();
                    }
                }
            }
        }

        /// <summary>
        /// 参与人表中找到业务的参与者，如果找到的参与人跟登录Icp用户匹配，就将其显示，否则显示一条新增数据
        /// </summary>
        /// <param name="operationID"></param>
        /// <param name="operationType"></param>
        private void GetAssistantInfoInServiceDB(Guid operationID, OperationType operationType, bool isClearDataSource)
        {
            List<StaffObjects> dataList = FCMCommonService.GetAssistantList(Guid.Empty, operationID, operationType);
            if (dataList == null || dataList.Count <= 0)
            {
                this.Insert(CreateStaffInfo(operationID, operationType, null, null));
            }
            else
            {
                var staffInfo = dataList.Find(item => item.UserID.Equals(LocalData.UserInfo.LoginID));
                if (staffInfo != null)
                {
                    if (isClearDataSource)
                        this.Insert(staffInfo);
                }
                else
                    this.Insert(CreateStaffInfo(operationID, operationType, null, null));
            }
        }

        private Dictionary<string, Guid> _Roles;
        private Dictionary<string, Guid> InitRoles(DataTable dtList)
        {
            _Roles = new Dictionary<string, Guid>();
            if (dtList != null && dtList.Rows.Count > 0)
            {
                foreach (DataRow row in dtList.Rows)
                {
                    string roleName = row.Field<String>("RoleName");
                    _Roles.Add(roleName, row.Field<Guid>("RoleID"));
                    cmbRole.Items.Add(roleName);
                }
            }
            return _Roles;
        }

        public StaffObjects CreateStaffInfo(Guid operationID, OperationType operationType, Guid? formID, FormType? formType)
        {
            isNewDataSource = true;
            return new StaffObjects()
             {
                 OperationID = operationID,
                 Ownersource = operationType,
                 FormID = formID,
                 FormType = formType,
                 UserID = LocalData.UserInfo.LoginID,
                 IsDirty = true,
             };
        }

        private void DataBind(object value)
        {
            isNewDataSource = false;
            this.Insert(value);
        }

        public void Insert(object value)
        {
            if (bsStaff.DataSource == null)
            {
                bsStaff.DataSource = new List<StaffObjects>();
            }

            bsStaff.Insert(bsStaff.Count, value);
            gvStaffList.FocusedRowHandle = bsStaff.Count - 1;
            ResetBindings();
        }

        private void ResetBindings()
        {
            bsStaff.ResetBindings(false);
        }
        #endregion

        #region 事件

        /// <summary>
        /// 保存参与人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler("Command_Unknown_AssistantListSubmit")]
        public void Command_Unknown_AssistantListSubmit(object sender, EventArgs e)
        {
            if (DataList != null && DataList.Count > 0)
            {
                try
                {
                    List<StaffObjects> results = DataList.FindAll(item => item.IsDirty);
                    if (results != null && results.Count > 0)
                    {
                        for (int i = 0; i < results.Count; i++)
                        {
                            FCMCommonService.SaveAssistantInfo(results[i]);
                            results[i].IsDirty = false;
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(this.FindForm(), ex.Message);
                }

            }




        }



        private void OnNameComboBoxEditValueChanged(object sender, EventArgs e)
        {
            if (CurrentRow != null && CurrentRow.CanUpdate)
            {
                ListItem item = ((DevExpress.XtraEditors.ComboBoxEdit)sender).SelectedItem as ListItem;
                if (item != null)
                {
                    UserList info = userList.Find(o => o.ID.Equals(new Guid(item.Id)));
                    if (info != null)
                    {
                        SetFocusedRowData(info.EMail, info.Tel, info.ID);
                        FocusedDataRow.Name = LocalData.IsEnglish ? info.EName : info.CName;
                        FocusedDataRow.IsDirty = true;
                        ResetBindings();
                    }
                }
            }
        }

        private void OnRoleComboBoxEditValueChanged(object sender, EventArgs e)
        {
            //是新增数据，并且没有自动把数据带出来
            if (CurrentRow != null && CurrentRow.CanUpdate)
            {
                SetRoleName(sender);
                if (string.IsNullOrEmpty(CurrentRow.Name))
                {
                    ICP.Sys.ServiceInterface.DataObjects.UserInfo userInfo = userService.GetUserInfo(LocalData.UserInfo.LoginID);
                    if (userInfo != null)
                    {
                        SetNameComboxDataSource(CurrentRow.OperationID);
                        FocusedDataRow.Name = LocalData.IsEnglish ? userInfo.EName : userInfo.CName;
                        SetFocusedRowData(userInfo.EMail, userInfo.Tel, userInfo.ID);
                        Workitem.Commands["Command_VisibilitySaveBarItem"].Execute();
                    }
                }
                SetRoleID(CurrentRow.Role);
                FocusedDataRow.IsDirty = true;
                ResetBindings();
                roleName = CurrentRow.Role;
            }
        }

        private void SetFocusedRowData(string mail, string tel, Guid userID)
        {
            FocusedDataRow.Mail = mail;
            FocusedDataRow.Tel = tel;
            FocusedDataRow.UpdateBy = LocalData.UserInfo.LoginID;
            FocusedDataRow.UserID = userID;
            FocusedDataRow.CreateDate = DateTime.Now;
            FocusedDataRow.CreateBy = LocalData.UserInfo.LoginID;
        }

        private void SetRoleName(object control)
        {
            object selectedValue = (control as ComboBoxEdit).SelectedItem;
            if (CurrentRow.Role == null)
                CurrentRow.Role = selectedValue.ToString();
            if (selectedValue.ToString() == string.Empty)
            {
                (control as ComboBoxEdit).SelectedItem = roleName;
                CurrentRow.Role = roleName;
            }
            else
                CurrentRow.Role = selectedValue.ToString();
        }

        private void SetRoleID(string roleName)
        {
            if (_Roles != null && _Roles.Count > 0 && !string.IsNullOrEmpty(roleName))
            {
                Guid roleID;
                _Roles.TryGetValue(roleName, out roleID);
                FocusedDataRow.RoleID = roleID;
            }
        }

        private void OnGridViewFocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (CurrentRow != null)
            {
                if (!CurrentRow.CanUpdate)
                {
                    this.colName.OptionsColumn.AllowEdit = false;
                    this.colRole.OptionsColumn.AllowEdit = false;
                }
                else
                {
                    this.colName.OptionsColumn.AllowEdit = true;
                    this.colRole.OptionsColumn.AllowEdit = true;
                }
            }
        }

        private void gcStaff_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                popupMenu1.ShowPopup(MousePosition);
        }
        /// <summary>
        /// 单元格点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column == colMail)
            {
                if (e.CellValue == null) return;
                // 邮件发送的消息实体
                var message = new Message.ServiceInterface.Message
                    {
                        Type = MessageType.Email,
                        Way = MessageWay.Send,
                        SendTo = e.CellValue == null ? "" : e.CellValue.ToString(),
                        SendFrom = LocalData.UserInfo.EmailAddress,
                        UserProperties =
                            new Message.ServiceInterface.MessageUserPropertiesObject { Action = string.Empty },
                        Body = string.Empty,
                        Subject = string.Empty
                    };

                OutLookService.Send(message);
                message = null;
            }
        }

        /// <summary>
        /// 行样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvStaffList_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            StaffObjects row = gvStaffList.GetRow(e.RowHandle) as StaffObjects;
            if (row == null) return;

            Font font = e.Appearance.Font;
            if (row.CanUpdate)
            {
                if (ICP.Framework.CommonLibrary.Client.LocalData.ApplicationType ==
                ICP.Framework.CommonLibrary.Common.ApplicationType.ICP)
                    e.Appearance.Font = new Font(font, FontStyle.Bold);
                else
                    e.Appearance.Font = new Font(font, FontStyle.Regular);
            }
            else
                e.Appearance.Font = new Font(font, FontStyle.Regular);
        }


        void OnComboxParseEditValue(object sender, DevExpress.XtraEditors.Controls.ConvertEditValueEventArgs e)
        {
            e.Value = e.Value == null ? string.Empty : e.Value.ToString();
            e.Handled = true;
        }

        #endregion

    }
}

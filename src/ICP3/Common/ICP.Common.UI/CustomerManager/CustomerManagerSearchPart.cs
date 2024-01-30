//-----------------------------------------------------------------------
// <copyright file="CustomerManagerSearchPart.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.Common.UI.CustomerManager
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using ICP.Framework.ClientComponents.UIFramework;
    using Microsoft.Practices.CompositeUI.SmartParts;
    using Microsoft.Practices.CompositeUI;
    using ICP.Common.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Client;
    using ICP.Framework.CommonLibrary.Helper;
    using DevExpress.XtraEditors.Controls;
    using ICP.Framework.ClientComponents.Controls;

    /// <summary>
    /// 客户管理-搜索器面版
    /// <remarks>
    /// 在该面板只实现本界面的控制逻辑。
    /// 如果要与服务交互。需通过Controller交互.(最少知识原则)
    /// </remarks>
    /// </summary>
    [System.ComponentModel.ToolboxItem(false)]
    [SmartPart]
    public partial class CustomerManagerSearchPart:BaseSearchPart
    {
        Guid? _nullNodeofctrLocationID = null;

        #region 初始化
        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }
        public CustomerManagerSearchPart()
        {
            this.InitializeComponent();
            this.Disposed += delegate {
                this.OnConditionChanged = null;
                this.OnSearched = null;
                this.cmbType.OnFirstEnter -= this.OncmbTypeFirstEnter;
                this.cmbApplyState.OnFirstEnter -= this.OncmbApplyStateFirstEnter;
                this.ctrLocation.OnFirstTimeEnter -= this.OnctrLocationFirstEnter;
                if (this.WorkItem != null)
                {
                    this.WorkItem.Items.Remove(this);
                    this.WorkItem = null;
                }
            
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                this.InitControls();
            }
        }
        private void OnctrLocationFirstEnter(object sender, EventArgs e)
        {
            List<CountryProvinceList> datas = this.Controller.GetCountryProvinceList(
                 string.Empty,
                 string.Empty,
                 null,
                 true,
                 0);

            CountryProvinceList nullItem = new CountryProvinceList();
            nullItem.ID = Guid.NewGuid();
            _nullNodeofctrLocationID = nullItem.ID;
            nullItem.ParentID = null;
            nullItem.ParentName = string.Empty;
            nullItem.Type = CountryProvinceType.Country;
            nullItem.CName = string.Empty;
            nullItem.EName = string.Empty;
            datas.Insert(0, nullItem);

            ctrLocation.AllowMultSelect = false;
            ctrLocation.RootValue = Guid.Empty;
            ctrLocation.ParentMember = "ParentID";
            ctrLocation.ValueMember = "ID";
            ctrLocation.DisplayMember = LocalData.IsEnglish ? "EName" : "CName";
            ctrLocation.DataSource = datas;

            ctrLocation.InitSelectedNode(null);
        }
        private void OncmbTypeFirstEnter(object sender, EventArgs e)
        {
            List<EnumHelper.ListItem<CustomerType>> customerTypes = EnumHelper.GetEnumValues<CustomerType>(LocalData.IsEnglish);

            cmbType.Properties.BeginUpdate();

            cmbType.Properties.Items.Add(new ImageComboBoxItem(string.Empty, null));
            foreach (var item in customerTypes)
            {
                if (item.Value == CustomerType.Unknown)
                {
                    continue;
                }

                cmbType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbType.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish ? "AgentOfCarrier" : "承运人", (short)100, 0));

            cmbType.Properties.EndUpdate();
        }
        private void OncmbApplyStateFirstEnter(object sender, EventArgs e)
        {
            List<EnumHelper.ListItem<CustomerCodeApplyState>> customerCodeApplyState = EnumHelper.GetEnumValues<CustomerCodeApplyState>(LocalData.IsEnglish);

            cmbApplyState.Properties.BeginUpdate();

            cmbApplyState.Properties.Items.Add(new ImageComboBoxItem(string.Empty, null));
            foreach (var item in customerCodeApplyState)
            {
                if (item.Value == CustomerCodeApplyState.All)
                {
                    continue;
                }

                cmbApplyState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }

            cmbApplyState.Properties.EndUpdate();
        }
        private void InitControls()
        {
            //地点控件初始化
            this.ctrLocation.OnFirstTimeEnter += this.OnctrLocationFirstEnter;

            #region 区域控件初始化
            List<EnumHelper.ListItem<CodeApplicantArea>> areas = EnumHelper.GetEnumValues<CodeApplicantArea>(LocalData.IsEnglish);
            cmbArea.Properties.BeginUpdate();
            cmbArea.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(LocalData.IsEnglish ? "All" : "全部", null));
            foreach (var item in areas)
            {
                cmbArea.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }

            cmbArea.Properties.EndUpdate();
            if (LocalCommonServices.PermissionService.HaveActionPermission(CustomerManagerConstants.Common_checkCustomer))
            {
                if (LocalData.UserInfo.DefaultCompanyID == new Guid("5A827ADF-38C7-4A2F-99A7-AD717CE91718"))
                {
                    //越南公司
                    cmbArea.SelectedIndex = 3;
                }
                else
                {
                    ConfigureInfo configureInfo = this.Controller.ConfigureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID);
                    if (configureInfo.SolutionID == new Guid("b6e4dded-4359-456a-b835-e8401c910fd0"))
                    {
                        //远东区解决方案，属于远东区物流板块
                        cmbArea.SelectedIndex = 1;
                    }
                    else
                    {
                        //非远东区解决方案，属于北美区物流板块
                        cmbArea.SelectedIndex = 2;
                    }
                }
            }

            #endregion

            //类型控件初始化
            this.cmbType.OnFirstEnter += this.OncmbTypeFirstEnter;
    

            //申请开始,结束时间初始化
            dteFrom.DateTime = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified).Date.AddMonths(-1);
            dteTo.DateTime = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified).Date;

            //状态控件初始化
           // Utility.SetEnterToExecuteOnec(cmbCustomerState, delegate
           //{
               cmbCustomerState.Properties.BeginUpdate();

               List<EnumHelper.ListItem<CustomerStateType>> customerStateTypes = EnumHelper.GetEnumValues<CustomerStateType>(LocalData.IsEnglish);
               //cmbCustomerState.Properties.Items.Add(new ImageComboBoxItem(string.Empty, null));
               foreach (var item in customerStateTypes)
               {
                   cmbCustomerState.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
               }

               cmbCustomerState.Properties.EndUpdate();
               cmbCustomerState.SelectedIndex = 0;
           //});

            //申请状态初始化
               this.cmbApplyState.OnFirstEnter += this.OncmbApplyStateFirstEnter;
     
        }

        //重置所有控件
        private void ResetControls()
        {
            foreach (Control item in navBarGroupBase.Controls)
            {
                if (item is DevExpress.XtraEditors.TextEdit
                    && (item is DevExpress.XtraEditors.SpinEdit) == false
                    && (item is DevExpress.XtraEditors.ComboBoxEdit) == false

                    && item.Enabled == true
                    && (item as DevExpress.XtraEditors.TextEdit).Properties.ReadOnly == false)
                    item.Text = string.Empty;
            }

            this.ctrLocation.EditValue = null;
            this.cmbArea.EditValue = null;
            this.cmbApplyState.EditValue = null;
            this.cmbCustomerState.EditValue = null;
            this.cmbType.EditValue = null;

            this.txtCode.Focus();
        }
        #endregion

        #region 控制器

        /// <summary>
        /// 客户管理控制器
        /// </summary>
        public CustomerManagerController Controller
        {
            get
            {
                return ClientHelper.Get<CustomerManagerController, CustomerManagerController>();
            }
        }
        
        #endregion

        #region ISearchPart 接口

        /// <summary>
        /// 查询界面条件改变后触发该事件
        /// <remarks>
        /// 只有场景中需要该接口方法取值时,才需要实现该接口方法.
        /// 如果没用到该接口方法,为了代码的清洁可以清除.
        /// </remarks>
        /// </summary>
        public override event ConditionChangedHandler OnConditionChanged;

        /// <summary>
        /// 查询完成后,触发该事件
        /// <remarks>
        /// 查询完成后,一定要触发该事件
        /// </remarks>
        /// </summary>
        public override event SearchResultHandler OnSearched;

        /// <summary>
        /// 获取数据
        /// <remarks>
        /// 获取数据逻辑一定要实现在这里
        /// </remarks>
        /// </summary>
        /// <returns></returns>
        public override object GetData()
        {
            //区域
            Guid? areaID = null;
            if (cmbArea.EditValue != null)
            {
                if ((CodeApplicantArea)cmbArea.EditValue == CodeApplicantArea.FarEast)
                {
                    areaID = new Guid("FA56E82F-2352-E111-A359-0026551CA87B");
                }
                else if ((CodeApplicantArea)cmbArea.EditValue == CodeApplicantArea.NorthAmerica)
                {
                    areaID = new Guid("B22FE9E1-B33A-4E40-88C5-528EED76B314");
                }
                else if ((CodeApplicantArea)cmbArea.EditValue == CodeApplicantArea.Vietnam)
                {
                    areaID = new Guid("5A827ADF-38C7-4A2F-99A7-AD717CE91718");
                }
            }
          
            //状态
            CustomerStateType? customerState = CustomerStateType.Valid;
            if (cmbCustomerState.EditValue != null)
            {
                customerState = (CustomerStateType)cmbCustomerState.EditValue;
            }

            //类型
            CustomerType? customerType = null;
            bool? isAgentOfCarrier = null;
            if (cmbType.EditValue != null && cmbType.EditValue.ToString().Length > 0)
            {
                if (cmbType.EditValue.GetType() == typeof(short))
                {
                    isAgentOfCarrier = true;
                }
                else
                {
                    customerType = (CustomerType)cmbType.EditValue;
                }
            }

            //申请审核开始，结束时间
            DateTime? from = null, to = null;
            if (chkEnabled.Checked)
            {
                from = dteFrom.DateTime.Date;
                to = Utility.GetEndDate(dteTo.DateTime);
            }

            //审核状态
            CustomerCodeApplyState? codeApplyState = null;
            if (cmbApplyState.EditValue != null)
            {
                codeApplyState = (CustomerCodeApplyState)cmbApplyState.EditValue;
            }

            Guid? countryId = (Guid?)ctrLocation.GetSelectedValues("ParentID");
            Guid? provinceId = null;
            if (countryId != null)
            {
                if (ctrLocation.SelectedValue != null)
                {
                    provinceId = (Guid)ctrLocation.SelectedValue;
                }
            }
            else
            {
                if (ctrLocation.SelectedValue != null && (Guid)ctrLocation.SelectedValue != _nullNodeofctrLocationID)
                {
                    countryId = (Guid)ctrLocation.SelectedValue;
                }
            }

            List<CustomerList> list = this.Controller.GetCustomerList(txtCode.Text.Trim(),
                                                                      txtName.Text.Trim(),
                                                                      txtAddress.Text.Trim(),
                                                                      txtTel.Text.Trim(),
                                                                      txtFax.Text.Trim(),
                                                                      txtEmail.Text.Trim(),
                                                                      countryId,
                                                                      provinceId,
                                                                      customerState,
                                                                      customerType,
                                                                      isAgentOfCarrier,
                                                                      codeApplyState,
                                                                      areaID,
                                                                      from,
                                                                      to,
                                                                      int.Parse(numMax.Value.ToString()));
            return list;
        }

        /// <summary>
        /// 从外界向查询面版初始化值
        /// <remarks>
        /// 只有场景中需要该接口方法取值时,才需要实现该接口方法.
        /// 如果没用到该接口方法,为了代码的清洁可以清除.
        /// </remarks>
        /// </summary>
        /// <param name="values">初始化值</param>
        public override void Init(IDictionary<string, object> values)
        {
        }

        /// <summary>
        /// 触发工具栏按钮的查询事件(列入下拉工具栏按钮,文本框按钮)
        /// <remarks>
        /// 只有场景中需要该接口方法取值时,才需要实现该接口方法.
        /// 如果没用到该接口方法,为了代码的清洁可以清除.
        /// </remarks>
        /// </summary>
        public override void RaiseSearched()
        {
            if (this.OnSearched != null)
            {
                using (new CursorHelper())
                {
                    object datas = this.GetData();
                    this.OnSearched(this, datas);
                }
            }
        }

        #endregion

        #region 设计生成代码

        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraNavBar.NavBarGroup nbarBase;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupBase;
        private LWImageComboBoxEdit cmbType;
        private DevExpress.XtraEditors.LabelControl labGeography;
        private DevExpress.XtraEditors.LabelControl labType;
        private DevExpress.XtraEditors.ImageComboBoxEdit cmbCustomerState;
        private DevExpress.XtraEditors.SpinEdit numMax;
        private DevExpress.XtraEditors.LabelControl labMax;
        private DevExpress.XtraEditors.LabelControl labCustomerState;
        private DevExpress.XtraEditors.LabelControl labTel;
        private DevExpress.XtraEditors.LabelControl labName;
        private DevExpress.XtraEditors.LabelControl labEmail;
        private DevExpress.XtraEditors.LabelControl labFax;
        private DevExpress.XtraEditors.LabelControl labAddress;
        private DevExpress.XtraEditors.LabelControl labCode;
        private DevExpress.XtraEditors.TextEdit txtTel;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.TextEdit txtEmail;
        private DevExpress.XtraEditors.TextEdit txtFax;
        private DevExpress.XtraEditors.TextEdit txtAddress;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraNavBar.NavBarGroupControlContainer navBarGroupControlContainer1;
        private DevExpress.XtraEditors.DateEdit dteFrom;
        private DevExpress.XtraEditors.LabelControl labFrom;
        private DevExpress.XtraEditors.DateEdit dteTo;
        private DevExpress.XtraEditors.LabelControl labTo;
        private DevExpress.XtraEditors.CheckEdit chkEnabled;
        private DevExpress.XtraEditors.LabelControl labCompany;
        private  LWImageComboBoxEdit cmbApplyState;
        private DevExpress.XtraEditors.LabelControl labApplyState;
        private DevExpress.XtraNavBar.NavBarGroup nbarApply;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton btnReset;
        private ICP.Common.UI.Controls.LWComboBoxTree ctrLocation;
        private ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit cmbArea;
        private DevExpress.XtraEditors.SimpleButton btnSearch;

        private void InitializeComponent()
        {
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.nbarBase = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarGroupBase = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.ctrLocation = new ICP.Common.UI.Controls.LWComboBoxTree();
            this.cmbType = new LWImageComboBoxEdit();
            this.labGeography = new DevExpress.XtraEditors.LabelControl();
            this.labType = new DevExpress.XtraEditors.LabelControl();
            this.cmbCustomerState = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.numMax = new DevExpress.XtraEditors.SpinEdit();
            this.labMax = new DevExpress.XtraEditors.LabelControl();
            this.labCustomerState = new DevExpress.XtraEditors.LabelControl();
            this.labTel = new DevExpress.XtraEditors.LabelControl();
            this.labName = new DevExpress.XtraEditors.LabelControl();
            this.labEmail = new DevExpress.XtraEditors.LabelControl();
            this.labFax = new DevExpress.XtraEditors.LabelControl();
            this.labAddress = new DevExpress.XtraEditors.LabelControl();
            this.labCode = new DevExpress.XtraEditors.LabelControl();
            this.txtTel = new DevExpress.XtraEditors.TextEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.txtEmail = new DevExpress.XtraEditors.TextEdit();
            this.txtFax = new DevExpress.XtraEditors.TextEdit();
            this.txtAddress = new DevExpress.XtraEditors.TextEdit();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.navBarGroupControlContainer1 = new DevExpress.XtraNavBar.NavBarGroupControlContainer();
            this.cmbArea = new ICP.Framework.ClientComponents.Controls.LWImageComboBoxEdit();
            this.dteFrom = new DevExpress.XtraEditors.DateEdit();
            this.labFrom = new DevExpress.XtraEditors.LabelControl();
            this.dteTo = new DevExpress.XtraEditors.DateEdit();
            this.labTo = new DevExpress.XtraEditors.LabelControl();
            this.chkEnabled = new DevExpress.XtraEditors.CheckEdit();
            this.labCompany = new DevExpress.XtraEditors.LabelControl();
            this.cmbApplyState = new LWImageComboBoxEdit();
            this.labApplyState = new DevExpress.XtraEditors.LabelControl();
            this.nbarApply = new DevExpress.XtraNavBar.NavBarGroup();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnReset = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            this.navBarControl1.SuspendLayout();
            this.navBarGroupBase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctrLocation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCustomerState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFax.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            this.navBarGroupControlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbArea.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEnabled.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbApplyState.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.nbarBase;
            this.navBarControl1.Controls.Add(this.navBarGroupBase);
            this.navBarControl1.Controls.Add(this.navBarGroupControlContainer1);
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControl1.ExplorerBarGroupInterval = 10;
            this.navBarControl1.ExplorerBarGroupOuterIndent = 10;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.nbarBase,
            this.nbarApply});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 140;
            this.navBarControl1.Size = new System.Drawing.Size(236, 482);
            this.navBarControl1.TabIndex = 6;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // nbarBase
            // 
            this.nbarBase.Caption = "基础";
            this.nbarBase.ControlContainer = this.navBarGroupBase;
            this.nbarBase.Expanded = true;
            this.nbarBase.GroupClientHeight = 253;
            this.nbarBase.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarBase.Name = "nbarBase";
            // 
            // navBarGroupBase
            // 
            this.navBarGroupBase.Controls.Add(this.ctrLocation);
            this.navBarGroupBase.Controls.Add(this.cmbType);
            this.navBarGroupBase.Controls.Add(this.labGeography);
            this.navBarGroupBase.Controls.Add(this.labType);
            this.navBarGroupBase.Controls.Add(this.cmbCustomerState);
            this.navBarGroupBase.Controls.Add(this.numMax);
            this.navBarGroupBase.Controls.Add(this.labMax);
            this.navBarGroupBase.Controls.Add(this.labCustomerState);
            this.navBarGroupBase.Controls.Add(this.labTel);
            this.navBarGroupBase.Controls.Add(this.labName);
            this.navBarGroupBase.Controls.Add(this.labEmail);
            this.navBarGroupBase.Controls.Add(this.labFax);
            this.navBarGroupBase.Controls.Add(this.labAddress);
            this.navBarGroupBase.Controls.Add(this.labCode);
            this.navBarGroupBase.Controls.Add(this.txtTel);
            this.navBarGroupBase.Controls.Add(this.txtName);
            this.navBarGroupBase.Controls.Add(this.txtEmail);
            this.navBarGroupBase.Controls.Add(this.txtFax);
            this.navBarGroupBase.Controls.Add(this.txtAddress);
            this.navBarGroupBase.Controls.Add(this.txtCode);
            this.navBarGroupBase.Name = "navBarGroupBase";
            this.navBarGroupBase.Size = new System.Drawing.Size(212, 251);
            this.navBarGroupBase.TabIndex = 0;
            // 
            // ctrLocation
            // 
            this.ctrLocation.AllowMultSelect = false;
            this.ctrLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ctrLocation.DataSource = null;
            this.ctrLocation.DisplayMember = "CName";
            this.ctrLocation.Location = new System.Drawing.Point(61, 150);
            this.ctrLocation.Name = "ctrLocation";
            this.ctrLocation.ParentMember = "ParentID";
            this.ctrLocation.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.ctrLocation.Properties.PopupSizeable = false;
            this.ctrLocation.Properties.ShowPopupCloseButton = false;
            this.ctrLocation.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.ctrLocation.RootValue = 0;
            this.ctrLocation.SelectedValue = null;
            this.ctrLocation.Separator = ",";
            this.ctrLocation.Size = new System.Drawing.Size(148, 21);
            this.ctrLocation.TabIndex = 42;
            this.ctrLocation.ValueMember = "ID";
            // 
            // cmbType
            // 
            this.cmbType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbType.Location = new System.Drawing.Point(61, 174);
            this.cmbType.Name = "cmbType";
            this.cmbType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbType.Size = new System.Drawing.Size(148, 21);
            this.cmbType.TabIndex = 7;
            // 
            // labGeography
            // 
            this.labGeography.Location = new System.Drawing.Point(7, 153);
            this.labGeography.Name = "labGeography";
            this.labGeography.Size = new System.Drawing.Size(53, 14);
            this.labGeography.TabIndex = 41;
            this.labGeography.Text = "国家/省份";
            // 
            // labType
            // 
            this.labType.Location = new System.Drawing.Point(7, 177);
            this.labType.Name = "labType";
            this.labType.Size = new System.Drawing.Size(24, 14);
            this.labType.TabIndex = 11;
            this.labType.Text = "类型";
            // 
            // cmbCustomerState
            // 
            this.cmbCustomerState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCustomerState.Location = new System.Drawing.Point(61, 200);
            this.cmbCustomerState.Name = "cmbCustomerState";
            this.cmbCustomerState.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbCustomerState.Size = new System.Drawing.Size(148, 21);
            this.cmbCustomerState.TabIndex = 9;
            // 
            // numMax
            // 
            this.numMax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.numMax.EditValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numMax.Location = new System.Drawing.Point(61, 224);
            this.numMax.Name = "numMax";
            this.numMax.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.numMax.Properties.IsFloatValue = false;
            this.numMax.Properties.Mask.EditMask = "N00";
            this.numMax.Properties.MaxValue = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numMax.Size = new System.Drawing.Size(148, 21);
            this.numMax.TabIndex = 10;
            // 
            // labMax
            // 
            this.labMax.Location = new System.Drawing.Point(7, 227);
            this.labMax.Name = "labMax";
            this.labMax.Size = new System.Drawing.Size(48, 14);
            this.labMax.TabIndex = 9;
            this.labMax.Text = "最大行数";
            // 
            // labCustomerState
            // 
            this.labCustomerState.Location = new System.Drawing.Point(7, 203);
            this.labCustomerState.Name = "labCustomerState";
            this.labCustomerState.Size = new System.Drawing.Size(24, 14);
            this.labCustomerState.TabIndex = 7;
            this.labCustomerState.Text = "状态";
            // 
            // labTel
            // 
            this.labTel.Location = new System.Drawing.Point(7, 81);
            this.labTel.Name = "labTel";
            this.labTel.Size = new System.Drawing.Size(24, 14);
            this.labTel.TabIndex = 6;
            this.labTel.Text = "电话";
            // 
            // labName
            // 
            this.labName.Location = new System.Drawing.Point(7, 33);
            this.labName.Name = "labName";
            this.labName.Size = new System.Drawing.Size(24, 14);
            this.labName.TabIndex = 6;
            this.labName.Text = "名称";
            // 
            // labEmail
            // 
            this.labEmail.Location = new System.Drawing.Point(7, 129);
            this.labEmail.Name = "labEmail";
            this.labEmail.Size = new System.Drawing.Size(24, 14);
            this.labEmail.TabIndex = 5;
            this.labEmail.Text = "邮件";
            // 
            // labFax
            // 
            this.labFax.Location = new System.Drawing.Point(7, 105);
            this.labFax.Name = "labFax";
            this.labFax.Size = new System.Drawing.Size(24, 14);
            this.labFax.TabIndex = 5;
            this.labFax.Text = "传真";
            // 
            // labAddress
            // 
            this.labAddress.Location = new System.Drawing.Point(7, 57);
            this.labAddress.Name = "labAddress";
            this.labAddress.Size = new System.Drawing.Size(24, 14);
            this.labAddress.TabIndex = 5;
            this.labAddress.Text = "地址";
            // 
            // labCode
            // 
            this.labCode.Location = new System.Drawing.Point(7, 9);
            this.labCode.Name = "labCode";
            this.labCode.Size = new System.Drawing.Size(24, 14);
            this.labCode.TabIndex = 5;
            this.labCode.Text = "代码";
            // 
            // txtTel
            // 
            this.txtTel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTel.Location = new System.Drawing.Point(61, 78);
            this.txtTel.Name = "txtTel";
            this.txtTel.Size = new System.Drawing.Size(148, 21);
            this.txtTel.TabIndex = 3;
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(61, 30);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(148, 21);
            this.txtName.TabIndex = 1;
            // 
            // txtEmail
            // 
            this.txtEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEmail.Location = new System.Drawing.Point(61, 126);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(148, 21);
            this.txtEmail.TabIndex = 5;
            // 
            // txtFax
            // 
            this.txtFax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFax.Location = new System.Drawing.Point(61, 102);
            this.txtFax.Name = "txtFax";
            this.txtFax.Size = new System.Drawing.Size(148, 21);
            this.txtFax.TabIndex = 4;
            // 
            // txtAddress
            // 
            this.txtAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAddress.Location = new System.Drawing.Point(61, 54);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(148, 21);
            this.txtAddress.TabIndex = 2;
            // 
            // txtCode
            // 
            this.txtCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCode.Location = new System.Drawing.Point(61, 6);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(148, 21);
            this.txtCode.TabIndex = 0;
            // 
            // navBarGroupControlContainer1
            // 
            this.navBarGroupControlContainer1.Controls.Add(this.cmbArea);
            this.navBarGroupControlContainer1.Controls.Add(this.dteFrom);
            this.navBarGroupControlContainer1.Controls.Add(this.labFrom);
            this.navBarGroupControlContainer1.Controls.Add(this.dteTo);
            this.navBarGroupControlContainer1.Controls.Add(this.labTo);
            this.navBarGroupControlContainer1.Controls.Add(this.chkEnabled);
            this.navBarGroupControlContainer1.Controls.Add(this.labCompany);
            this.navBarGroupControlContainer1.Controls.Add(this.cmbApplyState);
            this.navBarGroupControlContainer1.Controls.Add(this.labApplyState);
            this.navBarGroupControlContainer1.Name = "navBarGroupControlContainer1";
            this.navBarGroupControlContainer1.Size = new System.Drawing.Size(212, 129);
            this.navBarGroupControlContainer1.TabIndex = 0;
            // 
            // cmbArea
            // 
            this.cmbArea.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbArea.Location = new System.Drawing.Point(61, 3);
            this.cmbArea.Name = "cmbArea";
            this.cmbArea.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbArea.Size = new System.Drawing.Size(148, 21);
            this.cmbArea.TabIndex = 48;
            // 
            // dteFrom
            // 
            this.dteFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dteFrom.EditValue = null;
            this.dteFrom.Enabled = false;
            this.dteFrom.Location = new System.Drawing.Point(61, 74);
            this.dteFrom.Name = "dteFrom";
            this.dteFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteFrom.Properties.Mask.EditMask = global::ICP.Common.UI.Resources.Resource_EN.RERH;
            this.dteFrom.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteFrom.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteFrom.Size = new System.Drawing.Size(148, 21);
            this.dteFrom.TabIndex = 44;
            // 
            // labFrom
            // 
            this.labFrom.Location = new System.Drawing.Point(7, 77);
            this.labFrom.Name = "labFrom";
            this.labFrom.Size = new System.Drawing.Size(24, 14);
            this.labFrom.TabIndex = 46;
            this.labFrom.Text = "开始";
            // 
            // dteTo
            // 
            this.dteTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dteTo.EditValue = null;
            this.dteTo.Enabled = false;
            this.dteTo.Location = new System.Drawing.Point(61, 101);
            this.dteTo.Name = "dteTo";
            this.dteTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dteTo.Properties.Mask.EditMask = global::ICP.Common.UI.Resources.Resource_EN.RERH;
            this.dteTo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dteTo.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.dteTo.Size = new System.Drawing.Size(148, 21);
            this.dteTo.TabIndex = 45;
            // 
            // labTo
            // 
            this.labTo.Location = new System.Drawing.Point(7, 104);
            this.labTo.Name = "labTo";
            this.labTo.Size = new System.Drawing.Size(24, 14);
            this.labTo.TabIndex = 47;
            this.labTo.Text = "结束";
            // 
            // chkEnabled
            // 
            this.chkEnabled.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chkEnabled.Location = new System.Drawing.Point(1, 52);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Properties.Caption = "申请时间";
            this.chkEnabled.Size = new System.Drawing.Size(185, 19);
            this.chkEnabled.TabIndex = 43;
            this.chkEnabled.CheckedChanged += new System.EventHandler(this.chkEnabled_CheckedChanged);
            // 
            // labCompany
            // 
            this.labCompany.Location = new System.Drawing.Point(7, 6);
            this.labCompany.Name = "labCompany";
            this.labCompany.Size = new System.Drawing.Size(24, 14);
            this.labCompany.TabIndex = 41;
            this.labCompany.Text = "区域"; 
            // 
            // cmbApplyState
            // 
            this.cmbApplyState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbApplyState.Location = new System.Drawing.Point(61, 29);
            this.cmbApplyState.Name = "cmbApplyState";
            this.cmbApplyState.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbApplyState.Size = new System.Drawing.Size(148, 21);
            this.cmbApplyState.TabIndex = 1;
            // 
            // labApplyState
            // 
            this.labApplyState.Location = new System.Drawing.Point(6, 32);
            this.labApplyState.Name = "labApplyState";
            this.labApplyState.Size = new System.Drawing.Size(48, 14);
            this.labApplyState.TabIndex = 7;
            this.labApplyState.Text = "审核状态";
            // 
            // nbarApply
            // 
            this.nbarApply.Caption = "审核信息";
            this.nbarApply.ControlContainer = this.navBarGroupControlContainer1;
            this.nbarApply.Expanded = true;
            this.nbarApply.GroupClientHeight = 131;
            this.nbarApply.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.ControlContainer;
            this.nbarApply.Name = "nbarApply";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.btnReset);
            this.panelControl1.Controls.Add(this.btnSearch);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 482);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(236, 55);
            this.panelControl1.TabIndex = 7;
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReset.Location = new System.Drawing.Point(25, 15);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 0;
            this.btnReset.Text = "重置(&R)";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSearch.Location = new System.Drawing.Point(116, 15);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "查询(&S)";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // CustomerManagerSearchPart
            // 
            this.Controls.Add(this.navBarControl1);
            this.Controls.Add(this.panelControl1);
            this.Name = "CustomerManagerSearchPart";
            this.Size = new System.Drawing.Size(236, 537);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            this.navBarControl1.ResumeLayout(false);
            this.navBarGroupBase.ResumeLayout(false);
            this.navBarGroupBase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ctrLocation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCustomerState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEmail.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFax.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            this.navBarGroupControlContainer1.ResumeLayout(false);
            this.navBarGroupControlContainer1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbArea.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dteTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEnabled.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbApplyState.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        #region 事件处理

        //重置
        private void btnReset_Click(object sender, EventArgs e)
        {
            this.ResetControls();
        }

        //查询
        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.RaiseSearched();
        }

        private void chkEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkEnabled.Checked)
            {
                dteFrom.Enabled = true;
                dteTo.Enabled = true;

                dteFrom.DateTime = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified).Date.AddMonths(-1);
                dteTo.DateTime = DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified).AddDays(1).Date.AddMilliseconds(-1);
            }
            else
            {
                dteFrom.Enabled = false;
                dteTo.Enabled = false;

                dteFrom.EditValue = null;
                dteTo.EditValue = null;
            }
        }

        #endregion
    }
}

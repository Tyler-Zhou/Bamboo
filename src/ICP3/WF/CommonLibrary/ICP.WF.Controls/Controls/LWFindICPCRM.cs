using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using Microsoft.Practices.CompositeUI.WinForms;
using ICP.WF.ServiceInterface;
using ICP.Common.ServiceInterface;
using ICP.Sys.ServiceInterface;
using ICP.WF.Controls.Form;
using ICP.Framework.ClientComponents.Forms;
using ICP.Framework.ClientComponents.Controls;
using ICP.WF.ServiceInterface.DataObject;
using System.Data;


namespace ICP.WF.Controls
{
    /// <summary>
    /// 搜索控件
    /// </summary>
    [ToolboxBitmap(typeof(BaseEdit), "Bitmaps256.TextEdit.bmp")]
    [DefaultProperty("LWFindICPCRM"),
    SRDescription("LWFindICPCRM"),
    SRTitle("LWFindICPCRM")]
    [Serializable()]
    public class LWFindICPCRM : TextEdit, IBindingService, IValidateService, IColumn, IEventService
    {
        #region 初始化


        public LWFindICPCRM()
        {
            this.BackColor = Color.LightYellow;
            this.Disposed += delegate
            {
                if (this.customerFinder != null)
                {
                    this.customerFinder.Dispose();
                    this.customerFinder = null;
                }
            };
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

        }

        #endregion

        #region    IEventService

        public EventData Event { get; set; }
        /// <summary>
        /// 可以处理的事件类型
        /// </summary>
        public EventType EventType
        {
            get;
            set;
        }

        /// <summary>
        /// 要绑定的原控件数据源里面对应的属性。
        /// </summary>
        public string SourceDataProperty
        {
            get;
            set;
        }

        /// <summary>
        /// 显示格式传txtName.Text=Name:{Name},Tel:{Tel}.
        /// </summary>
        public string FormartString
        {
            get;
            set;
        }

        #endregion

        #region IBindingService接口成员

        string _dataProperty;
        [Browsable(false)]
        public string DataProperty
        {
            get
            {
                return _dataProperty;
            }
            set
            {
                _dataProperty = value;
            }
        }

        string _controlProperty = "Text";
        [Browsable(true)]
        [RefreshProperties(RefreshProperties.All)]
        [TypeConverter(typeof(DataBindingControlPropertyConverter))]
        [SRCategory("DataBinding"), SRDescription("ControlProperty")]
        public string ControlProperty
        {
            get
            {
                return _controlProperty;
            }
            set
            {
                _controlProperty = value;
            }
        }


        [Browsable(false)]
        public System.Type DataPropertyType
        {
            get
            {
                if (this.ControlProperty.Equals("Value"))
                {
                    return typeof(Guid);
                }
                else
                {
                    return typeof(string);
                }
            }
        }

        public string[] GetCanBindingControlProperty()
        {
            return new string[] { "Text", "InitDataSourceObject" };
        }

        private System.Data.DataTable mainDataSource;
        public void Binding(object datasource)
        {
            this.DataBindings.Clear();
            if (datasource != null
                 && this.FiledType != FieldType.None
                && string.IsNullOrEmpty(DataProperty) == false
                && string.IsNullOrEmpty(ControlProperty) == false)
            {
                this.mainDataSource = datasource as System.Data.DataTable;
                this.DataBindings.Add(ControlProperty, datasource, DataProperty);
            }

            //if (this.Value == null || this.Value==System.DBNull.Value) return;

            //IWorkFlowExtendService extendService=null;
            //LWBaseForm vParent = GetWFParentForm(this);
            //if (vParent != null)
            //{
            //    extendService = (IWorkFlowExtendService)vParent.ServiceContainer.Get(typeof(IWorkFlowExtendService));
            //}

            //if (extendService == null) return;

            //Guid id = new Guid(this.Value.ToString());
            //if (id != Guid.Empty)
            //{
            //    List<WFCustomerInfo> list = extendService.GetCRMCustomerList(null, id, string.Empty, LocalData.IsEnglish);
            //    if (list != null && list.Count > 0)
            //    {
            //        this.Text = LocalData.IsEnglish ? list[0].EName : list[0].CName;
            //    }
            //}
        }
        #endregion

        #region IValidateService接口实现

        public bool RuntimeValidate(System.Windows.Forms.ErrorProvider errorTip, List<string> errors)
        {
            bool isSucc = true;
            if (errors == null) errors = new List<string>();

            if (mainDataSource != null && string.IsNullOrEmpty(this.DataProperty) == false)
            {
                //必须填写验证
                if (mainDataSource.Columns[DataProperty].AllowDBNull == false)
                {
                    object val = this.GetType().GetProperty(this.ControlProperty).GetValue(this, null);
                    if (val == null || string.IsNullOrEmpty(val.ToString()))
                    {
                        errorTip.SetError(this, Utility.GetString("MustInput", "必须填写"));
                        errors.Add(Utility.GetString("MustInput", "必须填写"));
                        isSucc = false;
                    }
                }

                //数据类型格式验证


                //长度验证
            }

            return isSucc;
        }

        public bool DesignValidate(List<string> errors)
        {
            if (errors == null) errors = new List<string>();
            bool isSucc = true;

            //if (this.SearchMember==null)
            //{
            //    errors.Add(Utility.GetString("NecessaryToProperty", "Necessary to set up [SearchMember] property", this.Name, "SearchMember"));
            //    isSucc = false;
            //}

            //if (this.ValueMember==null)
            //{
            //    errors.Add(Utility.GetString("NecessaryToProperty", "Necessary to set up [ValueMember] property", this.Name, "ValueMember"));
            //    isSucc = false;
            //}

            //if (this.DisplayMember==null)
            //{
            //    errors.Add(Utility.GetString("NecessaryToProperty", "Necessary to set up [DisplayMember] property", this.Name, "DisplayMember"));
            //    isSucc = false;
            //}
            return isSucc;
        }


        #endregion

        #region 自定义属性

        #region 基本
        [SRCategory("DataCustom"), ICP.Framework.CommonLibrary.Attributes.ICPBrowsable(true)]
        public new bool Visible
        {
            get
            {
                return base.Visible;
            }
            set
            {
                base.Visible = value;
            }
        }
        /// <summary>
        /// 只读
        /// </summary>
        [Browsable(true)]
        [RefreshProperties(RefreshProperties.All)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [SRDisplayName("ReadOnly"), ICPBrowsable(true), SRCategory("Base"), SRDescription("ReadOnlyDesc")]
        public bool ReadOnly
        {
            get { return base.Properties.ReadOnly; }
            set
            {
                base.Properties.ReadOnly = value;
            }
        }

        /// <summary>
        /// 名称
        /// </summary>
        [SRDisplayName("Name"),
        SRDescription("Name"),
        ICPBrowsable(true),
        SRCategory("Base")]
        public new string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }

        /// <summary>
        /// 值
        /// </summary>
        [SRDisplayName("EditValue"),
        SRDescription("EditValue"),
        SRCategory("Base")]
        public override object EditValue
        {
            get
            {
                return base.EditValue;
            }
            set
            {
                base.EditValue = value;
            }
        }


        #endregion

        #region 布局属性

        /// <summary>
        /// 布局
        /// </summary>
        [SRDisplayName("Dock"),
        ICPBrowsable(true),
        SRDescription("Dock"),
        SRCategory("Layout")]
        public override DockStyle Dock
        {
            get
            {
                return base.Dock;
            }
            set
            {
                base.Dock = value;
            }
        }

        /// <summary>
        /// 停靠
        /// </summary>
        [SRDisplayName("Anchor"),
        ICPBrowsable(true),
        SRCategory("Layout")]
        public new AnchorStyles Anchor
        {
            get { return base.Anchor; }
            set { base.Anchor = value; }
        }

        int columnSpan = 1;
        [RefreshProperties(RefreshProperties.All)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [SRDisplayName("ColumnSpan"),
        ICPBrowsable(true),
        SRCategory("Layout"),
        SRDescription("ColumnSpan")]
        public int ColumnSpan
        {
            get { return columnSpan; }
            set
            {
                if (value != columnSpan)
                {
                    columnSpan = value;

                    if (this.Parent != null && this.Parent is LWTableLayoutPanel)
                    {
                        LWTableLayoutPanel panel = this.Parent as LWTableLayoutPanel;
                        panel.SetColumnSpan(this, value);
                    }
                }
            }
        }

        int rowSpan = 1;
        [RefreshProperties(RefreshProperties.All)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [SRDisplayName("RowSpan"),
        ICPBrowsable(true),
        SRCategory("Layout"),
        SRDescription("RowSpan")]
        public int RowSpan
        {
            get { return rowSpan; }
            set
            {
                if (value != rowSpan)
                {
                    rowSpan = value;

                    if (this.Parent != null && this.Parent is LWTableLayoutPanel)
                    {
                        LWTableLayoutPanel panel = this.Parent as LWTableLayoutPanel;
                        panel.SetRowSpan(this, value);
                    }
                }
            }
        }

        #endregion

        #region 外观属性

        /// <summary>
        /// 字体
        /// </summary>
        [SRDisplayName("DispFont"),
        ICPBrowsable(true),
        SRCategory("Appearance"),
        SRDescription("Font")]
        public new Font Font
        {
            get { return base.Font; }
            set { base.Font = value; }
        }

        /// <summary>
        /// 前景颜色
        /// </summary>
        [SRDisplayName("DispForeColor"),
        ICPBrowsable(true),
        SRCategory("Appearance")]
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
            }
        }

        #endregion

        #region 行为

        /// <summary>
        /// Tab顺序
        /// </summary>
        [SRDisplayName("DispTabIndex"),
        ICPBrowsable(true),
        SRDescription("DispTabIndex")]
        public new int TabIndex
        {
            get { return base.TabIndex; }
            set { base.TabIndex = value; }
        }


        #endregion


        #region 控件关联
        string targetControlProperty;
        /// <summary>
        /// 更新目标控件对应的属性
        /// </summary>
        [Browsable(true)]
        [ICPBrowsable(true)]
        [Bindable(false)]
        [RefreshProperties(RefreshProperties.All)]
        [SRCategory("Event"), SRDescription("TargetsControlProperty")]
        [TypeConverter(typeof(ControlPropertyNameConverter))]
        public string TargetControlProperty
        {
            get { return targetControlProperty; }
            set { targetControlProperty = value; }
        }
        string targetControlName;
        /// <summary>
        /// 目标控件名称
        /// </summary>
        [Browsable(true)]
        [ICPBrowsable(true)]
        [Bindable(false)]
        [RefreshProperties(RefreshProperties.All)]
        [SRCategory("Event"), SRDescription("TargetsControlName")]
        [TypeConverter(typeof(ControlNameConverter))]
        public string TargetControlName
        {
            get { return targetControlName; }
            set { targetControlName = value; }
        }
        /// <summary>
        /// 目标控件
        /// </summary>
        [Browsable(false)]
        public IBindingService TargetControl
        {
            get
            {
                if (string.IsNullOrEmpty(TargetControlName) == false)
                {
                    LWBaseForm baseForm = GetWFParentForm(this);

                    return FindTargetControl(baseForm, TargetControlName) as IBindingService;
                }
                else
                {
                    return null;
                }
            }
        }


        Control findCtrl;
        Control FindTargetControl(Control parentCtrl, string name)
        {
            foreach (Control ctrl in parentCtrl.Controls)
            {
                if (ctrl.Name.ToLower().Equals(name.ToLower()))
                {
                    return ctrl;
                }
                else if (ctrl is LWDataGridView || ctrl is LWRadioGroup)
                {
                    continue;
                }
                else if (ctrl.HasChildren)
                {
                    findCtrl = FindTargetControl(ctrl, name);
                    if (findCtrl != null)
                    {
                        return findCtrl;
                    }
                }
            }

            return null;
        }
        #endregion


        private FindTextBoxType findType = FindTextBoxType.Customer;
        [Browsable(false)]
        [ICPBrowsable(false)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [SRCategory("Data"),
         SRDisplayName("FindTextFindTypeSD"),
         SRDescription("FindTextFindTypeDesc")]
        public FindTextBoxType FindType
        {
            get { return findType; }
            set { findType = value; }
        }

        private bool isCommission = false;
        /// <summary>
        /// 是否为佣金
        /// </summary>
        [ICPBrowsable(false)]
        public bool IsCommission
        {
            get { return isCommission; }
            set { isCommission = value; }
        }

        private object _value;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (_value != value)
                {
                    BindControlsByCustomerID(value);
                }
                _value = value;
            }
        }


        /// <summary>
        /// 根据用户ID来绑定其他的控件
        /// </summary>
        private void BindControlsByCustomerID(object value)
        {
            if (TargetControl == null || TargetControlProperty == null)
            {
                return;
            }
            System.Reflection.PropertyInfo dpi = TargetControl.GetType().GetProperty(TargetControlProperty);
            if (dpi == null)
            {
                return;
            }

            object val = value;
            if (val != null)
            {
                dpi.SetValue(TargetControl, val, null);
            }

        }

        SearchMember finderName = SearchMember.Name;
        [DefaultValue("")]
        [Browsable(false)]
        [ICPBrowsable(false)]
        [SRCategory("Data"),
        SRDisplayName("FindTextSearchMember"),
        SRDescription("FindTextSearchMember")]
        public SearchMember SearchMember
        {
            set { finderName = value; }
            get { return finderName; }
        }

        DipalyMember displayField = DipalyMember.Name;
        [DefaultValue("")]
        [Browsable(false)]
        [ICPBrowsable(false)]
        [SRCategory("Data"),
        SRDisplayName("FindTextDisplayMember"),
        SRDescription("FindTextDisplayMember")]
        public DipalyMember DisplayMember
        {
            set { displayField = value; }
            get { return displayField; }
        }


        ValueMember valueField = ValueMember.ID;
        [DefaultValue("")]
        [Browsable(false)]
        [ICPBrowsable(false)]
        [SRCategory("Data"),
        SRDisplayName("FindTextValueMember"),
        SRDescription("FindTextValueMember")]
        public ValueMember ValueMember
        {
            set { valueField = value; }
            get { return valueField; }
        }

        #endregion

        #region 本地方法

        LWBaseForm GetWFParentForm(Control control)
        {
            if (control.Parent is LWBaseForm)
            {
                return control.Parent as LWBaseForm;
            }
            else
            {
                return GetWFParentForm(control.Parent);
            }
        }

        #endregion

        #region 搜索器

        WorkItem workitem = null;
        IDataFinder finder;
        IDataFindClientService dfService;
        IDataFinderFactory finderFactory = null;
        IWorkFlowExtendService wfExtendService;
        Guid customerID = Guid.Empty;
        string customerName = string.Empty;

        private IDisposable customerFinder;
        private bool isLoad = false;
        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);

            if (isLoad)
            {
                return;
            }

            ShowSearchForm();

            isLoad = true;
        }

        /// <summary>
        /// 显示搜索器
        /// </summary>
        private void ShowSearchForm()
        {
            if (isLoad)
            {
                return;
            }

            #region 搜索器

            LWBaseForm vParent = GetWFParentForm(this);
            if (vParent != null && vParent.ServiceContainer != null)
            {
                finderFactory = (IDataFinderFactory)vParent.ServiceContainer.Get(typeof(IDataFinderFactory));
                workitem = (WorkItem)vParent.ServiceContainer.Get(typeof(WorkItem));
                wfExtendService = (IWorkFlowExtendService)vParent.ServiceContainer.Get(typeof(IWorkFlowExtendService));
                dfService = (IDataFindClientService)vParent.ServiceContainer.Get(typeof(IDataFindClientService));
            }
            if (workitem == null || wfExtendService == null)
            {
                return;
            }

            if (vParent.DataSource != null && vParent.DataSource.Tables["MainTable"] != null)
            {
                DataTable dt = vParent.DataSource.Tables["MainTable"];
                if (dt.Rows.Count == 0)
                {
                    return;
                }
                if (dt.Rows[0]["customerID"] != DBNull.Value && !string.IsNullOrEmpty(dt.Rows[0]["customerID"].ToString()))
                {
                    customerID = new Guid(dt.Rows[0]["customerID"].ToString());
                }
            }


            #region 注册搜索器
            string searchm = WWFConstants.CustomerExpenseTouchFinder;
            string[] customerResultValue = new string[] { "ID", "CustomerName" };
            string codeName = @"Code/Name";

            customerFinder = dfService.Register(this, searchm, codeName, customerResultValue,
                 GetSearchConditionCollection,
                delegate(object inputSource, object[] resultData)
                {
                    if (resultData != null)
                    {
                        if (resultData[0] != null)
                        {
                            foreach (object data in resultData)
                            {
                                WFCECRMCustomerTouchLogList item = data as WFCECRMCustomerTouchLogList;
                                if (item != null)
                                {
                                    this.Text = item.CustomerName;
                                    BindFormData(vParent, item);
                                    break;
                                }
                            }
                        }
                    }

                },
                delegate()
                {
                    this.Text = string.Empty;
                    this.Tag = Guid.Empty;
                },
                ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
            #endregion

            #endregion

            isLoad = true;
        }


        /// <summary>
        /// 搜索条件
        /// </summary>
        /// <returns></returns>
        public SearchConditionCollection GetSearchConditionCollection()
        {
            SearchConditionCollection item = new SearchConditionCollection();

            item.AddWithValue("CustomerID", this.Tag, false);
            item.AddWithValue("CustomerName", this.Text, false);

            return item;
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="vParent"></param>
        /// <param name="louchLogInfo"></param>
        private void BindFormData(LWBaseForm vParent, WFCECRMCustomerTouchLogList louchLogInfo)
        {
            DataSet ds = vParent.DataSource;
            if (ds != null && ds.Tables["MainTable"] != null)
            {
                DataTable dt = ds.Tables["MainTable"];
                if (dt.Rows.Count == 0)
                {
                    return;
                }

                dt.Rows[0]["CustomerId"] = louchLogInfo.CustomerID;
                dt.Rows[0]["CustomerName"] = louchLogInfo.CustomerName;
                dt.Rows[0]["LouchLogID"] = louchLogInfo.ID;
                dt.Rows[0]["ActionContent"] = louchLogInfo.Content;


                vParent.DataSource = ds;
            }

        }

        #endregion

        #region IColumn接口成员


        FieldType _filedType = FieldType.Other;
        /// <summary>
        /// 列类型
        /// </summary>
        [ICPBrowsable(true),
        SRCategory("DataCustom"),
        SRDisplayName("DispFiledType"),
        SRDescription("FiledType")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public FieldType FiledType
        {
            get { return _filedType; }
            set { _filedType = value; }
        }


        string columnName;
        /// <summary>
        /// 列名 
        /// </summary>
        [ICPBrowsable(true),
        SRDisplayName("DispColumnName"),
        SRCategory("DataCustom"),
        SRDescription("ColumnName")]
        public string ColumnName
        {
            get
            {
                return columnName;
            }
            set
            {
                columnName = value;
                this.DataProperty = columnName;

                if (string.IsNullOrEmpty(_caption))
                {
                    _caption = columnName;
                }
            }
        }

        string BuildColumnName(string val)
        {
            string _name = val;
            if (string.IsNullOrEmpty(val))
            {
                _name = Utility.GetPascalProperty(this.Name);
            }
            else
            {
                _name = val.Trim();
            }
            if (this._filedType == FieldType.Department)
            {
                _name = _name.Replace("DepartmentId", "").Replace("DepartmentName", "");

                if (this.DataPropertyType == typeof(System.Guid) || this.DataPropertyType == typeof(System.Guid?))
                {
                    _name = _name + "DepartmentId";
                }
                else
                {
                    _name = _name + "DepartmentName";
                }
            }
            else if (this._filedType == FieldType.User)
            {
                _name = _name.Replace("UserId", "").Replace("UserName", "");
                if (this.DataPropertyType == typeof(System.Guid) || this.DataPropertyType == typeof(System.Guid?))
                {
                    _name = _name + "UserId";
                }
                else
                {
                    _name = _name + "UserName";
                }
            }
            else if (this._filedType == FieldType.Job)
            {
                _name = _name.Replace("RoleId", "").Replace("RoleName", "");
                if (this.DataPropertyType == typeof(System.Guid) || this.DataPropertyType == typeof(System.Guid?))
                {
                    _name = _name + "RoleId";
                }
                else
                {
                    _name = _name + "RoleName";
                }
            }

            return _name;
        }


        /// <summary>
        /// 列类型
        /// </summary>
        Type columnType;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Type ColumnType
        {
            get
            {
                if (this.DesignMode)
                {
                    return this.DataPropertyType;
                }
                else
                {
                    return columnType;
                }
            }
            set { columnType = value; }
        }


        /// <summary>
        /// 最大长度
        /// </summary>
        [SRDisplayName("DispLength"),
        ICPBrowsable(true),
        SRCategory("DataCustom"),
        SRDescription("MaxLength")]
        public int MaxLength
        {
            get
            {
                if (this.ColumnType == typeof(string))
                {
                    return base.Properties.MaxLength;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                base.Properties.MaxLength = value;
            }
        }


        bool allowNull = true;
        /// <summary>
        /// 可为null
        /// </summary>
        [SRDisplayName("DispAllowNull"),
        ICPBrowsable(true),
        SRCategory("DataCustom"),
        SRDescription("DispAllowNull")]
        public bool AllowNull
        {
            get
            {
                return allowNull;
            }
            set
            {
                allowNull = value;
            }
        }

        private string _caption;
        /// <summary>
        /// 列标题
        /// </summary>
        [SRDisplayName("DispCaption"),
        ICPBrowsable(true),
        SRCategory("DataCustom"),
        SRDescription("Caption")]
        public string Caption
        {
            get
            {
                return _caption;
            }
            set
            {
                _caption = value;
            }
        }

        #endregion

        #region IValidateService接口实现
        /// <summary>
        /// 验证(运行时)
        /// </summary>
        /// <param name="errorTip">错误提示控件</param>
        /// <param name="errors">错误里表</param>
        /// <returns>通过验证返回true,没通过返回false</returns>
        public bool ValidateForRuntime(ErrorProvider errorTip, List<string> errors)
        {
            bool isSucc = true;
            if (errors == null) errors = new List<string>();

            if (mainDataSource != null && string.IsNullOrEmpty(this.DataProperty) == false)
            {
                object val = this.GetType().GetProperty(this.ControlProperty).GetValue(this, null);
                //必须填写验证
                if (mainDataSource.Columns[DataProperty].AllowDBNull == false)
                {
                    if (val == null ||
                        string.IsNullOrEmpty(val.ToString()) ||
                        (Utility.IsGuid(val.ToString()) && Utility.GuidIsNullOrEmpty(new Guid(val.ToString()))))
                    {
                        string message = Utility.GetString("MustInput", "必须填写");
                        message = (LocalData.IsEnglish ? this.columnName : this.Caption) + " " + message;

                        errorTip.SetError(this, message);
                        errors.Add(message);
                        isSucc = false;
                    }
                }
                //验证客户ID是否为空
                bool validateCustomerID = true;
                if (mainDataSource.Rows[0]["LouchLogID"] == null)
                {
                    validateCustomerID = false;
                }
                else
                {
                    object cid = mainDataSource.Rows[0]["LouchLogID"];
                    if (!Utility.IsGuid(cid.ToString()))
                    {
                        validateCustomerID = false;
                    }
                    else
                    {
                        if (new Guid(cid.ToString()) == Guid.Empty)
                        {
                            validateCustomerID = false;
                        }
                    }
                }
                if (validateCustomerID == false)
                {
                    string message = Utility.GetString("Please select the customer louch data", "请选择客户跟进纪录");
                    message = (LocalData.IsEnglish ? this.columnName : this.Caption) + " " + message;

                    errorTip.SetError(this, message);
                    errors.Add(message);
                    isSucc = false;
                }
            }

            return isSucc;
        }

        /// <summary>
        /// 验证(对于设计时)
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        public bool ValidateForDesign(List<string> errors)
        {
            return true;
        }
        #endregion

    }
}

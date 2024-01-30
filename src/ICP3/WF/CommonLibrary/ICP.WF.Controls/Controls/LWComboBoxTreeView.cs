

namespace ICP.WF.Controls
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Windows.Forms;
    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Controls;
    using DevExpress.XtraEditors.Repository;
    using ICP.Framework.CommonLibrary.Attributes;
    using ICP.Sys.ServiceInterface.DataObjects;
    using System.Linq;
    using ICP.Framework.CommonLibrary.Client;
    using ICP.Common.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Server;

    /// <summary>
    /// 下拉树型控件 
    /// </summary>
    [ToolboxBitmap(typeof(LWComboBoxTreeView), "Resources.TreeView.bmp")]
    [DefaultProperty("DataProperty"), 
    SRDescription("ComboBoxTreeViewDesc"),
    SRTitle("ComboBoxTreeViewDesc")]
    public class LWComboBoxTreeView : ICP.Framework.ClientComponents.Controls.LWComboBoxTree, IBindingService, IEventService, IValidateService,IColumn
    {
        #region 服务

        public ICP.Common.ServiceInterface.IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<ICP.Common.ServiceInterface.IConfigureService>();
            }
        }
        public ICP.Sys.ServiceInterface.IOrganizationService OrganizationService
        {
            get
            {
                return ServiceClient.GetService<ICP.Sys.ServiceInterface.IOrganizationService>();
            }
        }

        #endregion

        #region 本地变量

        private const int WM_LBUTTONDOWN = 0x201, WM_LBUTTONDBLCLK = 0x203;
        protected ToolStripDropDown dropDown;

        #endregion

        #region 构造函数


        public LWComboBoxTreeView()
            : base()
        {
            
        }


        [Description("Gets settings specific to the date editor."), Category(CategoryName.Properties), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new RepositoryItemPopupContainerEdit Properties { get { return base.Properties as RepositoryItemPopupContainerEdit; } }

         
        #endregion

        #region IValidateService接口实现
        /// <summary>
        /// 运行时的验证
        /// </summary>
        /// <param name="errorTip"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        public bool ValidateForRuntime(System.Windows.Forms.ErrorProvider errorTip, List<string> errors)
        {
            bool isSucc = true;
            if (errors == null) errors = new List<string>();

            if (mainDataSource != null && string.IsNullOrEmpty(this.DataProperty) == false)
            {
                //必须填写验证
                if (mainDataSource.Columns[DataProperty].AllowDBNull == false)
                {
                    object val = this.GetType().GetProperty(this.ControlProperty).GetValue(this, null);
                    if (val == null || 
                        string.IsNullOrEmpty(val.ToString()) ||
                         val.ToString() == Guid.Empty.ToString())
                    {
                        string message = Utility.GetString("MustInput", "必须填写");
                        message = (LocalData.IsEnglish ? this.columnName : this.Caption) + " " + message;

                        errorTip.SetError(this, message);
                        errors.Add(message);
                        isSucc = false;
                    }
                }
                //部门不能是PCH国际
                object depid = this.GetType().GetProperty(this.ControlProperty).GetValue(this, null);
                if (depid != null && new Guid(depid.ToString()) != Guid.Empty)
                {
                    if (depid.ToString().ToUpper() == "701ACD43-D49B-422B-83A9-ACB56B696995")
                    {
                        string message = Utility.GetString("not select PCH", "不能选择PCH国际");
                        message = (LocalData.IsEnglish ? this.columnName : this.Caption) + " " + message;

                        errorTip.SetError(this, message);
                        errors.Add(message);
                        isSucc = false;
                    }
                    OrganizationInfo regionInfo = OrganizationService.GetOrganizationInfo(new Guid(depid.ToString()));
                    if (regionInfo != null && (regionInfo.Type <= OrganizationType.Section || regionInfo.Type == OrganizationType.Group))
                    {
                        string message = Utility.GetString("Section and above departments cannot apply for forms.", "区域及以上的部门不能申请表单。");
                        message = (LocalData.IsEnglish ? this.columnName : this.Caption) + " " + message;

                        errorTip.SetError(this, message);
                        errors.Add(message);
                        isSucc = false;
                    }
                }
                else
                {
                    string message = Utility.GetString("MustInput", "必须填写");
                    message = (LocalData.IsEnglish ? this.columnName : this.Caption) + " " + message;

                    errorTip.SetError(this, message);
                    errors.Add(message);
                    isSucc = false;
                }
                //数据类型格式验证
                

                //长度验证
            }

            return isSucc;
        }
        /// <summary>
        /// 设计时的验证
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        public bool ValidateForDesign(List<string> errors)
        {
            if (errors == null) errors = new List<string>();
            bool isSucc = true;
            if (string.IsNullOrEmpty(this.Name))
            {
                return isSucc;
            }
            if (!string.IsNullOrEmpty(dataSourceName))
            {
                if (string.IsNullOrEmpty(this.CDisplayMember))
                {
                    errors.Add(Utility.GetString("NecessaryToProperty", "Necessary to set up [CDisplayMember] property", this.Name, Utility.GetString("CDisplayMember","CDisplayMember")));
                    isSucc = false;
                }

                if (string.IsNullOrEmpty(this.EDisplayMember))
                {
                    errors.Add(Utility.GetString("NecessaryToProperty", "Necessary to set up [EDisplayMember] property", this.Name, Utility.GetString("EDisplayMember","EDisplayMember")));
                    isSucc = false;
                }

                if (string.IsNullOrEmpty(this.ValueMember))
                {
                    errors.Add(Utility.GetString("NecessaryToProperty", "Necessary to set up [ValueMember] property", this.Name, Utility.GetString("ValueMember","ValueMember")));
                    isSucc = false;
                }

                if (string.IsNullOrEmpty(this.ParentMember))
                {
                    errors.Add(Utility.GetString("NecessaryToProperty", "Necessary to set up [ParentMember] property", this.Name, Utility.GetString("DispParentMember", "ParentMember")));
                    isSucc = false;
                }
            }


            return isSucc;
        }
        #endregion

        #region IBindingService
        string _dataProperty;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
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

        string _controlProperty= "Text";
        [SRCategory("DataBinding"),
        SRDisplayName("ControlProperty")]
        [ICPBrowsable(true)]
        [RefreshProperties(RefreshProperties.All)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [TypeConverter(typeof(DataBindingControlPropertyConverter))]
        [SRDescription("ControlProperty")]
        public string ControlProperty
        {
            get
            {
                if (DataPropertyType != typeof(string))
                {
                    return "SelectedValue";
                }
                else
                {
                    return _controlProperty;
                }
            }
            set
            {
                _controlProperty = value;
            }
        }

        [Browsable(false)]
        public Type DataPropertyType
        {
            get
            {
                if (string.IsNullOrEmpty(this.ValueMember) || this.DataSource == null)
                {
                    //没设置Valuemeber的是直接添加字符串数据源的

                    return typeof(string);
                }
                else
                {
                    //对于绑定BindingSource的，可以提取ValueMember的类型

                    IBindingSourceTypeService bs = this.DataSource as IBindingSourceTypeService;
                    if (bs != null)
                    {
                        System.Type type = bs.DataType;
                        if (type != null)
                        {
                            System.Reflection.PropertyInfo pi = type.GetProperty(this.ValueMember);
                            if (pi != null)
                            {
                                return pi.PropertyType;
                            }
                        }
                    }
                }
                return typeof(string);
            }
        }

        public string[] GetCanBindingControlProperty()
        {
            return new string[] { "Text", "SelectedValue" };
        }

        private System.Data.DataTable mainDataSource;
        public void Binding(object datasource)
        {
            if (string.IsNullOrEmpty(DataSourceName) == false)
            {
                BindingSource bs = (this.GetWFParentForm(this) as LWBaseForm).BindingSources[DataSourceName] as BindingSource;
                if (bs != null)
                {
                    this.DataSource = bs.DataSource;
                }
            }

            this.DataBindings.Clear();
            if (datasource != null
                 && this.FiledType != FieldType.None
                && string.IsNullOrEmpty(DataProperty) == false 
                && string.IsNullOrEmpty(ControlProperty) == false)
            {
                try{
                this.mainDataSource = datasource as System.Data.DataTable;
                this.DataBindings.Add(ControlProperty, datasource, DataProperty);
           
              }
                catch { }
            }
        }

        
        #endregion

        #region IEventService接口成员实现

        EventData _event;
        [ICPBrowsable(true), 
        Editor(typeof(EventDataTypeEditor), 
            typeof(UITypeEditor)), 
        RefreshProperties(RefreshProperties.All), 
        DefaultValue((string)null),
        SRCategory("Event"),
        SRDisplayName("Event"),]
        public EventData Event
        {
            get { return _event; }
            set { _event = value; }
        }

        EventType eventType = EventType.None;
        /// <summary>
        /// 事件定义区
        /// </summary>
        [ICPBrowsable(false),
        SRCategory("Event"),
        SRDisplayName("EventType"),
        SRDescription("Controlevent")]
        public EventType EventType
        {
            get { return eventType; }
            set { eventType = value; }
        }

        string sourceDataProperty;
        /// <summary>
        /// 本控件数据源里面的属性
        /// </summary>
        [ICPBrowsable(false), 
        SRCategory("Event"),
        SRDisplayName("SourceProperty"),
        SRDescription("ControlDataSourceProperties")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), DefaultValue("")]
        public string SourceDataProperty
        {
            get
            {
                return sourceDataProperty;
            }
            set
            {
                sourceDataProperty = value;
            }
        }


        string targetControlName;
        /// <summary>
        /// 目标控件名称
        /// </summary>
        [Browsable(true)]
        [Bindable(false)]
        [RefreshProperties(RefreshProperties.All)]
        [SRCategory("Event"), SRDescription("TargetsControlName")]
        [TypeConverter(typeof(ControlNameConverter))]
        public string TargetControlName
        {
            get { return targetControlName; }
            set { targetControlName = value; }
        }


        string targetControlProperty;
        /// <summary>
        /// 更新目标控件对应的属性
        /// </summary>
        [Browsable(true)]
        [Bindable(false)]
        [RefreshProperties(RefreshProperties.All)]
        [SRCategory("Event"), SRDescription("TargetsControlProperty")]
        [TypeConverter(typeof(ControlPropertyNameConverter))]
        public string TargetControlProperty
        {
            get { return targetControlProperty; }
            set { targetControlProperty = value; }
        }


        string formartString;
        /// <summary>
        /// 格式串
        /// </summary>
        [Browsable(false), SRCategory("Event"), Description("设置在目标控件中显示的格式:例子:名字:{Name}.电话{Tel}")]
        public string FormartString
        {
            get { return formartString; }
            set { formartString = value; }
        }


        [Browsable(false)]
        public IBindingService TargetControl
        {
            get
            {
                if (string.IsNullOrEmpty(TargetControlName) == false)
                {
                    return FindTargetControl(GetWFParentForm(this), TargetControlName) as IBindingService;
                }
                else
                {
                    return null;
                }
            }
        }


        Control GetWFParentForm(Control control)
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

        Control FindTargetControl(Control parentCtrl, string name)
        {
            foreach (Control ctrl in parentCtrl.Controls)
            {
                if (ctrl.Name.ToLower().Equals(name.ToLower()))
                {
                    return ctrl;
                }
                else if (ctrl is LWDataGridView
                    || ctrl is LWRadioGroup)
                {
                    continue;
                }
                else if (ctrl.HasChildren)
                {
                    return FindTargetControl(ctrl, name);
                }
            }

            return null;
        }

        #endregion

        #region 控件数据源设置与显示相关属性


        /// <summary>
        /// 名称
        /// </summary>
        [SRDisplayName("Name"),
        ICPBrowsable(true),
        SRDescription("Name"),
        SRCategory("Base")]
        public new string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }

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



        string dataSourceName;
        /// <summary>
        /// 对应bingdingsource的名称
        /// </summary>
        public string DataSourceName
        {
            get { return dataSourceName; }
            set 
            {
                dataSourceName = value;
            }
        }

        [SRCategory("DataSource"),
        ICPBrowsable(true),
        SRDisplayName("DataSource"),
        SRDescription("DataSource")]
        [AttributeProvider(typeof(IListSource))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public new object DataSource
        {
            get
            {
                if (this.DesignMode)
                {
                    if (string.IsNullOrEmpty(DataSourceName) == false)
                    {
                        if (base.DataSource is LWCompanys)
                        {
                            this.FiledType = FieldType.Department;
                        }
                        else if (base.DataSource is LWJobs)
                        {
                            this.FiledType = FieldType.Job;
                        }
                        else if (base.DataSource is LWUsers)
                        {
                            this.FiledType = FieldType.User;
                        }
                    }
                    else
                    {
                        base.DataSource = null;
                    }

                    if (base.DataSource == null && !string.IsNullOrEmpty(dataSourceName))
                    {
                        base.DataSource = this.Container.Components[dataSourceName] as BindingSource;
                    }

                    return base.DataSource;
                }

                return base.DataSource;
            }
            set
            {
                base.DataSource = value;

                if (this.DesignMode)
                {
                    if (value != null)
                    {
                        string pName = value.ToString();
                        if (pName.IndexOf("[") > 0)
                        {
                            dataSourceName = pName.Substring(0, pName.IndexOf("[") - 1).Trim();
                        }
                    }
                    else
                    {
                        dataSourceName = string.Empty;
                    }
                }
            }
        }


        [Browsable(false)]
        public new string DisplayMember
        {
            get
            {
                if (Utility.IsEnglish)
                {
                    return this.EDisplayMember;
                }
                else
                {
                    return this.CDisplayMember;
                }
            }
            set
            {
                base.DisplayMember = value;
            }
        }

        private string _cdisplayMember=string.Empty;
        [ICPBrowsable(true)]
        [SRCategory("Data"), 
        SRDescription("CDisplayMember"),
        SRDisplayName("CDisplayMember")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        public string CDisplayMember
        {
            get
            {
                return this._cdisplayMember;
            }
            set
            {
                this._cdisplayMember = value;
            }
        }

        private string _edisplayMember=string.Empty;
        [ICPBrowsable(true)]
        [SRCategory("Data"), 
        SRDescription("EDisplayMember"),
        SRDisplayName("EDisplayMember")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        public  string EDisplayMember
        {
            get
            {
                return this._edisplayMember;
            }
            set
            {
                this._edisplayMember = value;
            }
        }






        /// <summary>
        /// 值成员
        /// </summary>
        [SRCategory("Data"), 
        SRDescription("ValueMember"),
        SRDisplayName("ValueMember")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        [ICPBrowsable(true)]
        public new string ValueMember
        {
            get
            {
                return base.ValueMember;
            }
            set
            {
                base.ValueMember = value;
            }
        }


        private object _initParentKey;
        /// <summary>
        /// 根节点的父值 
        /// </summary>
        [ICPBrowsable(false)]
        public object InitParentKey
        {
            get
            {
                return this._initParentKey;
            }
            set
            {
                this._initParentKey = value;
            }
        }
       
        [ICPBrowsable(true)]
        [SRCategory("Data"), 
        SRDescription("ParentMember"),
        SRDisplayName("DispParentMember")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        public new string ParentMember
        {
            get
            {
                return base.ParentMember;
            }
            set
            {
                base.ParentMember = value;
            }
        }

        #endregion

        #region ComboBox重载

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (dropDown != null)
                {
                    dropDown.Dispose();
                    dropDown = null;
                }
            }
            base.Dispose(disposing);
        }

        #endregion

        #region 公共属性
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
        /// 下拉面板大小
        /// </summary>
        [SRCategory("DataCustom"), SRDisplayName("PopupFormSize"), ICP.Framework.CommonLibrary.Attributes.ICPBrowsable(true)]
        [Browsable(true)]
        public Size PopupFormSize
        {
            get { return base.Properties.PopupFormSize; }
            set { base.Properties.PopupFormSize = value; }
        }

        /// <summary>
        /// 文本编辑类型
        /// </summary>
        [SRCategory("DataCustom"), SRDisplayName("TextEditStyle"), ICP.Framework.CommonLibrary.Attributes.ICPBrowsable(true)]
        [Browsable(true)]
        public TextEditStyles TextEditStyle
        {
            get { return base.Properties.TextEditStyle; }
            set { base.Properties.TextEditStyle = value; }
        }

        /// <summary>
        /// 当前选择的值
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Bindable(true, BindingDirection.TwoWay)]
        [DefaultValue("")]
        [Browsable(false)]
        public new object SelectedValue
        {
            get
            {
                return base.SelectedValue;
            }
            set
            {
                base.SelectedValue = value;

                if (value != null)
                {
                    if (base.DataSource is List<OrganizationList>)
                    {
                        List<OrganizationList> orgList = base.DataSource as List<OrganizationList>;

                        if (value != null && Utility.IsGuid(value.ToString()))
                        {
                            var selectOrg = from o in orgList where o.ID == new Guid(value.ToString()) select o;
                            foreach (var org in selectOrg)
                            {
                                base.Text = org.FullName;
                                break;
                            }
                        }
                    }
                    else if (base.DataSource is List<SolutionGLCodeList>)
                    {
                        List<SolutionGLCodeList> glCodeList = base.DataSource as List<SolutionGLCodeList>;

                        if (value != null && Utility.IsGuid(value.ToString()))
                        {
                            var query = from o in glCodeList where o.ID == new Guid(value.ToString()) select o;
                            foreach (var gl in query)
                            {
                                base.Text = LocalData.IsEnglish ? gl.EfullName : gl.FullName;
                                break;
                            }
                        }
                    }
                }

               
            }
        }



        #endregion


        #region IColumn接口成员

        FieldType _filedType = FieldType.Other;
        /// <summary>
        /// 字段类型
        /// </summary>
        [ICPBrowsable(true),
        SRCategory("DataCustom"),
        SRDisplayName("FiledType")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public FieldType FiledType
        {
            get { return _filedType; }
            set { _filedType = value; }
        }


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

        string columnName=string.Empty;
        /// <summary>
        /// 列名 
        /// </summary>
        [ICPBrowsable(true)]
        [SRCategory("DataCustom"),
        SRDescription("ColumnName"),
        SRDisplayName("DispColumnName")]
        public string ColumnName
        {
            get
            {
                if (this.DesignMode)
                {
                    columnName = Utility.BuildSpecialColumnName(
                       columnName,
                       this.DataPropertyType,
                       this.FiledType);
                    this.DataProperty = columnName;
                }
                return columnName;
            }
            set
            {
                if (this.DesignMode)
                {
                    columnName = Utility.BuildSpecialColumnName(
                        value,
                        this.DataPropertyType,
                        this.FiledType);

                    this.DataProperty = columnName;

                    if (string.IsNullOrEmpty(_caption))
                    {
                        _caption = columnName;
                    }
                }
                else
                {
                    columnName = value;
                }
            }
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
            set
            {
                columnType = value;
            }
        }

        int maxLength = 50;
        /// <summary>
        /// 最大长度
        /// </summary>
        [ICPBrowsable(true)]
        [SRCategory("DataCustom"),
        SRDescription("MaxLength"),
        SRDisplayName("DispLength")]
        public int MaxLength
        {
            get
            {
                if (this.ColumnType == typeof(string))
                {
                    return maxLength;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                maxLength = value;
            }
        }

        private bool allowNull = true;
        /// <summary>
        /// 是否允许为null
        /// </summary>
        [ICPBrowsable(true)]
        [SRCategory("DataCustom"),
        SRDescription("AllowNull"),
        SRDisplayName("AllowNull")]
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
        [ICPBrowsable(true)]
        [SRCategory("DataCustom"),
        SRDescription("Caption"),
        SRDisplayName("DispCaption")]
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
    }


    [Serializable]
    public class LWCommonEventArgs<T> : EventArgs
    {
        public LWCommonEventArgs(T data)
        {
            Data = data;
        }

        public T Data { get; set; }
    }

    public class LWCommonCancelEventArgs<T> : CancelEventArgs
    {
        private T _data;
        internal LWCommonCancelEventArgs(T data)
        {
            _data = data;
        }
        public T Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
            }
        }
    }
}

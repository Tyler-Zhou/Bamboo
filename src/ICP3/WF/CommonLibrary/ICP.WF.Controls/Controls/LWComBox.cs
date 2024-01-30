
//-----------------------------------------------------------------------
// <copyright file="LWComBox.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
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
    using ICP.Common.ServiceInterface.DataObjects;
    using ICP.WF.ServiceInterface.DataObject;
    using ICP.Framework.CommonLibrary.Client;
    using System.Linq;
    using ICP.FAM.ServiceInterface.DataObjects;

    /// <summary>
    /// 组合框控件
    /// </summary>
    [ToolboxBitmap(typeof(BaseEdit), "Bitmaps256.ImageComboBoxEdit.bmp")]
    [DefaultProperty("DataProperty"), 
    SRDescription("ComBoxDesc"),
    SRTitle("ComBoxTitle")]
    [Serializable()]
    public class LWComBox : ImageComboBoxEdit, IBindingService, IEventService, IValidateService,IColumn
    {
        #region 构造函数



        public LWComBox()
            : base()
        {
            //this.Properties.Buttons.Add(new EditorButton(ButtonPredefines.Combo));
        }


        [Description("Gets settings specific to the date editor."), Category(CategoryName.Properties), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new RepositoryItemImageComboBox Properties { get { return base.Properties as RepositoryItemImageComboBox; } }




        #endregion

        #region IValidateService接口实现
        /// <summary>
        /// 验证(运行时)
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
                if (!mainDataSource.Columns[DataProperty].AllowDBNull)
                {
                    object val = this.GetType().GetProperty(this.ControlProperty).GetValue(this, null);
                    if (val == null || 
                        string.IsNullOrEmpty(val.ToString())||
                        val.ToString()==Guid.Empty.ToString())
                    {
                        string message = Utility.GetString("MustInput", "必须填写");
                        message = (LocalData.IsEnglish ? this.columnName : this.Caption) + " " + message;

                        errorTip.SetError(this, message);
                        errors.Add(message);
                        isSucc = false;
                    }
                }

                //数据类型格式验证


                //长度验证
            }

            return isSucc;
        }

        /// <summary>
        /// 验证(设计时)
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

            if (!string.IsNullOrEmpty(this.DataSourceName))
            {
                if (string.IsNullOrEmpty(this.CDisplayMember))
                {
                    errors.Add(Utility.GetString("NecessaryToProperty", "Necessary to set up [CDisplayMember] property", this.Name, Utility.GetString("CDisplayMember","CDisplayMember")));
                    isSucc = false;
                }

                if (string.IsNullOrEmpty(this.EDisplayMember))
                {
                    errors.Add(Utility.GetString("NecessaryToProperty", "Necessary to set up [EDisplayMember] property", this.Name, Utility.GetString("EDisplayMember","CDisplayMember")));
                    isSucc = false;
                }

                if (string.IsNullOrEmpty(this.ValueMember))
                {
                    errors.Add(Utility.GetString("NecessaryToProperty", "Necessary to set up [ValueMember] property", this.Name, Utility.GetString("ValueMember","CDisplayMember")));
                    isSucc = false;
                }
            }

            return isSucc;
        }

        #endregion


        #region 自定义属性
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


        string dataSourceName;
        /// <summary>
        /// 该控件的数据源名
        /// </summary>
        [Browsable(false)]
        public string DataSourceName
        {
            get { return dataSourceName; }
            set { dataSourceName = value; }
        }

        object dataSouce;
        /// <summary>
        /// 该控件的数据源
        /// </summary>
        [SRCategory("DataSource"), SRDescription("DataSource")]
        [DefaultValue("")]
        [ICP.Framework.CommonLibrary.Attributes.ICPBrowsable(true), SRDisplayName("DataSource")]
        [AttributeProvider(typeof(IListSource))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public  object DataSource
        {
            get
            {
                if (this.DesignMode)
                {
                    if (string.IsNullOrEmpty(DataSourceName) == false)
                    {
                        dataSouce = this.Container.Components[dataSourceName] as BindingSource;
                        if (dataSouce == null)
                        {
                            return dataSouce;
                        }

                        if (dataSouce is LWCompanys)
                        {
                            this.FiledType = FieldType.Department;
                        }
                        else if (dataSouce is LWJobs)
                        {
                            this.FiledType = FieldType.Job;
                        }
                        else if (dataSouce is LWUsers)
                        {
                            this.FiledType = FieldType.User;
                        }
                    }
                    else
                    {
                        dataSouce = null;
                    }
                }

                return dataSouce;
            }
            set
            {
                dataSouce = value;

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
                else
                {
                    this.Properties.Items.Clear();
                    if (IsAddNullRow)
                    {
                        this.Properties.Items.Add(new ImageComboBoxItem(string.Empty, null));
                    }
                    if (this.FiledType == FieldType.Department)
                    {
                        #region 组织结构
                        List<OrganizationList> list = dataSouce as List<OrganizationList>;
                        if (list != null)
                        {
                            foreach (OrganizationList org in list)
                            {
                                if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish)
                                {
                                    this.Properties.Items.Add(new ImageComboBoxItem(org.EShortName, org.ID));
                                }
                                else
                                {
                                    this.Properties.Items.Add(new ImageComboBoxItem(org.CShortName, org.ID));
                                }
                            }
                        }
                        #endregion
                    }
                    else if (this.FiledType == FieldType.Job)
                    {
                        #region 职位
                        List<JobList> list = dataSouce as List<JobList>;
                        if (list != null)
                        {
                            foreach (JobList job in list)
                            {
                                if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish)
                                {
                                    this.Properties.Items.Add(new ImageComboBoxItem(job.EName, job.ID));
                                }
                                else
                                {
                                    this.Properties.Items.Add(new ImageComboBoxItem(job.CName, job.ID));
                                }
                            }
                        }
                        #endregion
                    }
                    else if (this.FiledType == FieldType.User)
                    {
                        #region 员工
                        List<UserList> list = dataSouce as List<UserList>;
                        if (LocalData.IsEnglish)
                        {
                            list = (from d in list orderby d.EName ascending select d).ToList();
                        }
                        else
                        {
                            list = (from d in list orderby d.CName ascending select d).ToList();
                        }
                        if (list != null)
                        {
                            foreach (UserList user in list)
                            {
                                if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish)
                                {
                                    this.Properties.Items.Add(new ImageComboBoxItem(user.EName, user.ID));
                                }
                                else
                                {
                                    this.Properties.Items.Add(new ImageComboBoxItem(user.CName, user.ID));
                                }
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        if (dataSouce is List<CurrencyList>)
                        {
                            #region 币种
                            List<CurrencyList> list = dataSouce as List<CurrencyList>;
                            if (list != null)
                            {
                                CurrencyList RMBCurrrency = list.Find(delegate(CurrencyList item) { return item.Code == "RMB"; });
                                CurrencyList USDCurrrency = list.Find(delegate(CurrencyList item) { return item.Code == "USD"; });
                                CurrencyList HKDCurrrency = list.Find(delegate(CurrencyList item) { return item.Code == "HKD"; });

                                foreach (CurrencyList currency in list)
                                {
                                    if (currency != RMBCurrrency &&
                                        currency != USDCurrrency &&
                                        currency != HKDCurrrency)
                                    {
                                        this.Properties.Items.Add(new ImageComboBoxItem(currency.Code, currency.ID));
                                    }
                                }
                                if (HKDCurrrency != null)
                                {
                                    this.Properties.Items.Insert(0, new ImageComboBoxItem(HKDCurrrency.Code, HKDCurrrency.ID));
                                }
                                if (USDCurrrency != null)
                                {
                                    this.Properties.Items.Insert(0, new ImageComboBoxItem(USDCurrrency.Code, USDCurrrency.ID));
                                }
                                if (RMBCurrrency != null)
                                {
                                    this.Properties.Items.Insert(0, new ImageComboBoxItem(RMBCurrrency.Code, RMBCurrrency.ID));
                                }

                            }
                            #endregion
                        }
                        else if (dataSouce is List<ChargingCodeList>)
                        {
                            #region  费用项目
                            List<ChargingCodeList> list = dataSouce as List<ChargingCodeList>;
                            if (list != null)
                            {
                                foreach (ChargingCodeList charge in list)
                                {
                                    if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish)
                                    {
                                        this.Properties.Items.Add(new ImageComboBoxItem(charge.EName, charge.ID));
                                    }
                                    else
                                    {
                                        this.Properties.Items.Add(new ImageComboBoxItem(charge.CName, charge.ID));
                                    }
                                }
                            }
                            #endregion
                        }
                        else if (dataSouce is List<DictCodeData>)
                        {
                            #region 报销性质&调动类型
                            List<DictCodeData> list = dataSouce as List<DictCodeData>;
                            if (list != null)
                            {
                                foreach (DictCodeData item in list)
                                {
                                    if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish)
                                    {
                                        this.Properties.Items.Add(new ImageComboBoxItem(item.EName, item.Key.ToString()));
                                    }
                                    else
                                    {
                                        this.Properties.Items.Add(new ImageComboBoxItem(item.CName, item.Key.ToString()));
                                    }
                                }
                            }
                            #endregion
                        }
                        else if (dataSouce is List<BankAccountList>)
                        {
                            #region 银行帐号
                            List<BankAccountList> list = dataSouce as List<BankAccountList>;
                            if (list != null)
                            {
                                foreach (BankAccountList bankAccount in list)
                                {
                                     this.Properties.Items.Add(new ImageComboBoxItem(bankAccount.CurrencyName, bankAccount.ID));
                                }
                            }
                            #endregion
                        }
                        else if (dataSouce is List<DataDictionaryList>)
                        {
                            #region 数据字典
                            List<DataDictionaryList> list = dataSouce as List<DataDictionaryList>;
                            if (list != null)
                            {
                                foreach (DataDictionaryList datadic in list)
                                {
                                    this.Properties.Items.Add(new ImageComboBoxItem(LocalData.IsEnglish?datadic.EName:datadic.CName, datadic.ID));
                                }
                            }
                            #endregion
                        }
                        //车辆 
                       

                    }
                   
          
                }


            }
        }

        string edisplayMember;
        /// <summary>
        /// 英文显示成员
        /// </summary>
        [SRCategory("DataSource"), SRDescription("EDisplayMember")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [ICP.Framework.CommonLibrary.Attributes.ICPBrowsable(true), SRDisplayName("EDisplayMember")]
        [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        public string EDisplayMember
        {
            get
            {
                return edisplayMember;
            }
            set
            {
                edisplayMember = value;
            }
        }

        private bool isAddNullRow;
        [ICPBrowsable(true)]
        public bool IsAddNullRow
        {
            get
            {
                return isAddNullRow;
            }
            set
            {
                isAddNullRow = value;
            }
        }
        /// <summary>
        /// 大小
        /// </summary>
        [SRDisplayName("DispSize"), ICPBrowsable(true), SRDescription("DescSize"), SRCategory("Base")]
        public new Size Size
        {
            get
            {
                return base.Size;
            }
            set
            {
                base.Size = value;
            }
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

        string cdisplayMember;
        /// <summary>
        /// 中文显示成员
        /// </summary>
        [SRCategory("DataSource"), SRDescription("CDisplayMember")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [ICP.Framework.CommonLibrary.Attributes.ICPBrowsable(true), 
        SRDisplayName("CDisplayMember")]
        [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        public string CDisplayMember
        {
            get
            {
                return cdisplayMember;
            }
            set
            {
                cdisplayMember = value;
            }
        }

  


        string valueMember;
        /// <summary>
        /// 值成员
        /// </summary>
        [SRCategory("DataSource"), Description("ValueMember")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [ICP.Framework.CommonLibrary.Attributes.ICPBrowsable(true), SRDisplayName("ValueMember")]
        [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        public string ValueMember
        {
            get
            {
                return valueMember;
            }
            set
            {
                valueMember = value;
            }
        }


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

        string _controlProperty = "SelectedValue";
        [ICP.Framework.CommonLibrary.Attributes.ICPBrowsable(true), 
        SRDisplayName("ControlProperty")]
        [RefreshProperties(RefreshProperties.All)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
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
                if (string.IsNullOrEmpty(this.ValueMember) || this.dataSouce == null)
                {
                    //没设置Valuemeber的是直接添加字符串数据源的


                    return typeof(string);
                }
                else
                {
                    //对于绑定BindingSource的，可以提取ValueMember的类型
                    IBindingSourceTypeService bs = this.dataSouce as IBindingSourceTypeService;
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
            return new string[] { "Text", "EditValue", "SelectedValue", "InitDataSourceObject" };
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
                try
                {
                    this.mainDataSource = datasource as System.Data.DataTable;
                    this.DataBindings.Add(ControlProperty, datasource, DataProperty);

                }
                catch { }
            }
        }
        #endregion


        #region 事件处理区


        EventData _event;
        [ICP.Framework.CommonLibrary.Attributes.ICPBrowsable(true), SRDisplayName("Event")]
        [Editor(typeof(EventDataTypeEditor), typeof(UITypeEditor)), RefreshProperties(RefreshProperties.All), DefaultValue((string)null), SRCategory("Event")]
        public EventData Event 
        { 
            get { return _event; } 
            set { _event = value; } 
        }


        EventType eventType = EventType.None;
        /// <summary>
        /// 事件定义区
        /// </summary>
        [ICP.Framework.CommonLibrary.Attributes.ICPBrowsable(true), SRDisplayName("EventType")]
        [SRCategory("Event"), SRDescription("Controlevent")]
        public EventType EventType
        {
            get { return eventType; }
            set { eventType = value; }
        }

        public object SelectedValue
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

        string sourceDataProperty;
        /// <summary>
        /// 本控件数据源里面的属性
        /// </summary>
        [SRDisplayName("SourceProperty")]
        [SRCategory("Event"), SRDescription("ControlDataSourceProperties")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), DefaultValue("")]
        [ICP.Framework.CommonLibrary.Attributes.ICPBrowsable(true)]
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
        [ICP.Framework.CommonLibrary.Attributes.ICPBrowsable(true), SRDisplayName("DispTargetControlName")]
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
        [ICP.Framework.CommonLibrary.Attributes.ICPBrowsable(true), SRDisplayName("DispTargetControlProperty")]
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
        //[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
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
                if (!string.IsNullOrEmpty(TargetControlName))
                {
                    cl = null;
                    FindTargetControl(GetWFParentForm(this), TargetControlName);

                    if (cl != null)
                    {
                        return cl as IBindingService;
                    }
                    else
                    {
                        return null;
                    }

               
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
        Control cl;
        void FindTargetControl(Control parentCtrl, string name)
        {

            foreach (Control ctrl in parentCtrl.Controls)
            {
                if (ctrl.Name.ToLower().Equals(name.ToLower()))
                {
                    cl=ctrl;
                }
                else if (ctrl is LWDataGridView
                    || ctrl is LWRadioGroup)
                {
                    continue;
                }
                else if (ctrl.HasChildren)
                {
                     FindTargetControl(ctrl, name);
                }
            }

        }

       

        object initDataObject;
        /// <summary>
        /// 根据该属性改变,刷新数据源
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object InitDataSourceObject
        {
            get
            {
                return initDataObject;
            }
            set
            {
                if (initDataObject != value)
                {
                    initDataObject = value;

                    //不时邦定到bingdingsource的不用刷新数据源
                    if (string.IsNullOrEmpty(DataSourceName)) return;
                    if (this.dataSouce == null) return;

                    //在非WFForm窗体为父窗体的中不用刷新数据
                    LWBaseForm parent = (LWBaseForm)this.GetWFParentForm(this);
                    if (parent == null) return;

                    //根据设置值刷新数据源
                    IInitiDataService initSvc = parent.BindingSources[DataSourceName] as IInitiDataService;
                    if (initSvc != null)
                    {
                        Dictionary<string, object> vals = new Dictionary<string, object>();
                        vals.Add("InitDataId", value);
                        initSvc.InitData(parent.ServiceContainer, vals);

                        BindingSource bs = parent.BindingSources[DataSourceName] as BindingSource;
                        if (bs != null)
                        {
                            this.DataSource = bs.DataSource;
                        }
                    }
                }
            }
        }


        protected  void OnSelectedIndexChanged(System.EventArgs e)
        {

            if (this.Event != null 
                && this.Event.EventType == EventType.SelectedValueChanged)
            {
                if (this.SelectedItem == null) return;

                LWBaseForm wf = (LWBaseForm)GetWFParentForm(this);

                this.Event.Excute(this.SelectedItem, wf.DataSource.Tables[0]);
                wf.BindingContext[wf.DataSource].EndCurrentEdit();
            }

           
           // base.OnSelectedIndexChanged(e);
        }

        protected override void OnLostFocus(System.EventArgs e)
        {
            if (this.Event != null 
                && this.Event.EventType == EventType.LostFocus)
            {
                if (this.SelectedItem == null) return;

                LWBaseForm wf = (LWBaseForm)GetWFParentForm(this);

                this.Event.Excute(this.SelectedItem, wf.DataSource.Tables[0]);
                wf.BindingContext[wf.DataSource].EndCurrentEdit();
            }

            base.OnLostFocus(e);
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
            set
            {
                try
                {
                    _filedType = value;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
         
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
                        _caption = value;
                    }
                }
                else
                {
                    columnName = value;
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
}

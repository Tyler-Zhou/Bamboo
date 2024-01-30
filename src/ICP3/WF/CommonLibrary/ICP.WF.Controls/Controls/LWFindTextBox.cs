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
using ICP.FAM.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;


namespace ICP.WF.Controls
{
    /// <summary>
    /// 搜索控件
    /// </summary>
    [ToolboxBitmap(typeof(BaseEdit), "Bitmaps256.TextEdit.bmp")]
    [SRDescription("FindTextBoxDesc"), SRTitle("FindTextBox")]
    [Serializable()]
    public class LWFindTextBox : TextEdit, IBindingService, IValidateService, IColumn
    {
        #region 服务

        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        #endregion

        #region 初始化


        public LWFindTextBox()
        {
            this.Disposed += delegate
            {

                if (this.userFinder != null)
                {
                    this.userFinder.Dispose();
                    this.userFinder = null;
                }

                if (this.customerFinder != null)
                {
                    this.customerFinder.Dispose();
                    this.customerFinder = null;
                }

                if (this.glCodeFinder != null)
                {
                    this.glCodeFinder.Dispose();
                    this.glCodeFinder = null;
                }

            };
        }
        private bool isLoad = false;
        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);

            if (isLoad)
            {
                return;
            }

            #region 搜索器

            IDataFinderFactory finderFactory = null;
            WorkItem workitem = null;
            LWBaseForm vParent = GetWFParentForm(this);
            if (vParent != null)
            {
                finderFactory = (IDataFinderFactory)vParent.ServiceContainer.Get(typeof(IDataFinderFactory));
                workitem = (WorkItem)vParent.ServiceContainer.Get(typeof(WorkItem));
                dfService = (IDataFindClientService)vParent.ServiceContainer.Get(typeof(IDataFindClientService));
            }

            if (workitem == null || dfService == null)
            {
                return;
            }

            string dism = string.Empty;
            string valm = string.Empty;
            string searchm = string.Empty;
            string searchKey = string.Empty;
            string codeName = @"Code/Name";
            switch (findType)
            {
                case FindTextBoxType.Customer:
                    #region 客户
                    searchm = CommonFinderConstants.CustoemrFinder;
                    //if (displayField == DipalyMember.Name)
                    //{
                    //    if (Utility.IsEnglish)
                    //    {
                    //        dism = "EName";
                    //    }
                    //    else
                    //    {
                    //        dism = "CName";
                    //    }
                    //}
                    //else
                    //{
                    //    dism = "Code";
                    //}

                    //if (finderName == SearchMember.Name)
                    //{
                    //    if (Utility.IsEnglish)
                    //    {
                    //        searchm = "EName";
                    //    }
                    //    else
                    //    {
                    //        searchm = "CName";
                    //    }
                    //}
                    //else
                    //{
                    //    searchm = "Code";
                    //}

                    //if (valueField == ValueMember.Name)
                    //{
                    //    if (Utility.IsEnglish)
                    //    {
                    //        valm = "EName";
                    //    }
                    //    else
                    //    {
                    //        valm = "CName";
                    //    }
                    //}
                    //else if (valueField == ValueMember.Code)
                    //{
                    //    valm = "Code";
                    //}
                    //else
                    //{
                    //    valm = "Id";
                    //}
                    #endregion

                    #region 注册搜索器

                    string[] customerResultValue = new string[] { "ID", "Code", "EName", "CName", "Type", "TradeTermID", "TradeTermName", "State", "CheckedState", "Term" };
                    //string codeName = @"Code/Name";

                    customerFinder = dfService.Register(this, searchm, codeName, customerResultValue,
                        delegate(object inputSource, object[] resultData)
                        {
                            Guid id = new Guid(resultData[0].ToString());
                            this.Tag = id;
                            this.Value = id;
                            this.Text = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                        },
                        delegate()
                        {
                            this.Text = string.Empty;
                            this.Tag = Guid.Empty;
                        },
                        ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
                    #endregion
                    break;
                case FindTextBoxType.User:
                    #region 用户
                    searchm = SystemFinderConstants.UserFinder;
                    //if (displayField == DipalyMember.Name)
                    //{
                    //    dism = "Name";
                    //}
                    //else
                    //{
                    //    dism = "Code";
                    //}

                    //if (finderName == SearchMember.Name)
                    //{
                    //    searchm = "Name";
                    //}
                    //else
                    //{
                    //    searchm = "Code";

                    //}
                    //if (valueField == ValueMember.Name)
                    //{
                    //    valm = "Name";
                    //}
                    //else if (valueField == ValueMember.Code)
                    //{
                    //    valm = "Code";
                    //}
                    //else
                    //{
                    //    valm = "Id";
                    //}
                    #endregion

                    #region 注册搜索器
                    string[] userResultValue = new string[] { "ID", "Code", "EName", "CName", "EMail" };
                    //string codeName = @"Code/Name";

                    userFinder = dfService.Register(this, searchm, codeName, userResultValue,
                        delegate(object inputSource, object[] resultData)
                        {
                            Guid id = new Guid(resultData[0].ToString());
                            this.Tag = id;
                            this.Value = id;
                            this.Text = LocalData.IsEnglish ? resultData[2].ToString() : resultData[3].ToString();
                        },
                        delegate()
                        {
                            this.Text = string.Empty;
                            this.Tag = Guid.Empty;
                        },
                        ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
                    #endregion
                    break;
                case FindTextBoxType.GLCode:
                    #region 会计科目
                    searchm = FAMFinderConstants.GLCodeFinder;
                    #endregion

                    #region 注册搜索器
                    string[] glCodeResultValue = new string[] { "ID", "Code", "GLCodeName" };
                    //string codeName = @"Code/Name";

                    glCodeFinder = dfService.Register(this, searchm, codeName, glCodeResultValue, GetSearchCondition,
                         delegate(object inputSource, object[] resultData)
                         {
                             Guid id = new Guid(resultData[0].ToString());
                             this.Tag = id;
                             this.Value = id;
                             this.Text = resultData[2].ToString();
                         },
                         delegate()
                         {
                             this.Text = string.Empty;
                             this.Tag = Guid.Empty;
                         },
                         ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace);
                    #endregion
                    break;
            }
            #endregion

            isLoad = true;
        }

        private Guid GetSolutionID()
        {
            Guid solutionID = Guid.Empty;
            ConfigureInfo configureInfo = ConfigureService.GetCompanyConfigureInfo(LocalData.UserInfo.DefaultCompanyID);
            if (configureInfo != null)
            {
                solutionID = configureInfo.SolutionID;
            }
            return solutionID;
        }

        SearchConditionCollection GetSearchCondition()
        {
            SearchConditionCollection conditions = new SearchConditionCollection();
            conditions.AddWithValue("SolutionID", GetSolutionID(), false);
            conditions.AddWithValue("OnlyLeaf", true, false);
            return conditions;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

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

        string _controlProperty = "Value";
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
            return new string[] { "Value", "Text" };
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

            if (ValueMember == ValueMember.ID)
            {
                if (this.Value == null || this.Value == System.DBNull.Value) return;

                IWorkFlowExtendService extendService = null;
                LWBaseForm vParent = GetWFParentForm(this);
                if (vParent != null)
                {
                    extendService = (IWorkFlowExtendService)vParent.ServiceContainer.Get(typeof(IWorkFlowExtendService));
                }

                if (extendService == null) return;

                string dism;
                if (FindType == FindTextBoxType.User)
                {
                    if (displayField == DipalyMember.Name)
                    {
                        dism = "Name";
                    }
                    else
                    {
                        dism = "Code";
                    }
                    if (this.Value != null)
                    {
                        Guid id = new Guid(this.Value.ToString());
                        if (id != null && id != Guid.Empty)
                        {
                            this.Text = extendService.GetUserInfoByIDProperty(id, dism);
                        }
                    }
                }
                else if (FindType == FindTextBoxType.Customer)
                {
                    if (displayField == DipalyMember.Name)
                    {
                        if (Utility.IsEnglish)
                        {
                            dism = "EName";
                        }
                        else
                        {
                            dism = "CName";
                        }
                    }
                    else
                    {
                        dism = "Code";
                    }

                    if (this.Value != null)
                    {
                        Guid id = new Guid(this.Value.ToString());

                        if (id != null && id != Guid.Empty)
                        {
                            this.Text = extendService.GetCustomerInfo(id, dism);
                        }
                    }
                }
                else if (findType == FindTextBoxType.GLCode)
                {
                    if (displayField == DipalyMember.Name)
                    {
                        if (Utility.IsEnglish)
                        {
                            dism = "EName";
                        }
                        else
                        {
                            dism = "CName";
                        }
                    }
                    else
                    {
                        dism = "Code";
                    }

                    if (this.Value != null)
                    {
                        Guid id = new Guid(this.Value.ToString());

                        if (id != null && id != Guid.Empty)
                        {
                            //this.Text = extendService.GetCustomerInfo(id, dism);
                            this.Text = ConfigureService.GetSolutionGLCodeInfoNew(id, LocalData.IsEnglish).GLCodeName;
                        }
                    }
                }
            }
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

        private FindTextBoxType findType = FindTextBoxType.Customer;
        [Browsable(true)]
        [ICPBrowsable(true)]
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
                _value = value;
            }
        }


        SearchMember finderName = SearchMember.Name;
        [DefaultValue("")]
        [Browsable(true)]
        [ICPBrowsable(true)]
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
        [Browsable(true)]
        [ICPBrowsable(true)]
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
        [Browsable(true)]
        [ICPBrowsable(true)]
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

        #region 事件处理

        private IDisposable customerFinder, userFinder, glCodeFinder;
        IDataFinder finder;
        IDataFindClientService dfService;

        void finder_DataChoosed(object sender, DataFindEventArgs e)
        {
            if (valueField == ValueMember.ID)
            {
                this.Tag = this.Value = e.Data[0] == null ? Guid.Empty : (Guid)e.Data[0];
            }
            else
            {
                this.Tag = this.Value = e.Data[0] == null ? string.Empty : (string)e.Data[0];
            }
            this.Text = (string)e.Data[1];
            // finder.Unwrap.FindForm().Close();
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
                //必须填写验证
                if (mainDataSource.Columns[DataProperty].AllowDBNull == false)
                {
                    object val = this.GetType().GetProperty(this.ControlProperty).GetValue(this, null);
                    if (val == null
                        || string.IsNullOrEmpty(val.ToString())
                        || (Utility.IsGuid(val.ToString()) && Utility.GuidIsNullOrEmpty(new Guid(val.ToString()))))
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

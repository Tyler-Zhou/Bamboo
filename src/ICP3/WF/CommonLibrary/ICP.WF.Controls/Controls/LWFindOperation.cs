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
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary;


namespace ICP.WF.Controls
{
    /// <summary>
    /// 搜索控件
    /// </summary>
    [ToolboxBitmap(typeof(BaseEdit), "Bitmaps256.TextEdit.bmp")]
    [DefaultProperty("LWFindOperationNo"),
    SRDescription("LWFindOperationNo"),
    SRTitle("LWFindOperationNo")]
    [Serializable()]
    public class LWFindOperationNo : TextEdit, IBindingService, IValidateService, IColumn, IEventService
    {
        #region 初始化


        public LWFindOperationNo()
        {
            this.BackColor = Color.LightYellow;
            this.Properties.ReadOnly = true;
            this.Disposed += delegate
            {

                if (this.organizationFinder != null)
                {
                    this.organizationFinder.Dispose();
                    this.organizationFinder = null;
                }
            };

        }
        private IDisposable organizationFinder;
        IDataFinder finder;
        IDataFindClientService dfService;
        IDataFinderFactory finderFactory = null;

        private bool isLoad = false;
        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);

            if (isLoad)
            {
                return;
            }
            ShowSearchForm();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (isLoad)
            {
                return;
            }
            ShowSearchForm();

            isLoad = true;
        }

        Guid customerID = Guid.Empty,CompanyID=Guid.Empty;
        string customerName = string.Empty;
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
                    customerName = dt.Rows[0]["Customer"] == null ? string.Empty : dt.Rows[0]["Customer"].ToString();
                }
                if (dt.Rows[0]["DepartmentID"] != DBNull.Value && !string.IsNullOrEmpty(dt.Rows[0]["DepartmentID"].ToString()))
                {
                    CompanyID = DataTypeHelper.GetGuid(dt.Rows[0]["DepartmentID"]);
                }
             
            }


            #region 注册搜索器
            string searchm = WWFConstants.CommissionFinder;
            string[] customerResultValue = new string[] { "ID", "OperactioNo" };
            string codeName = @"Code/Name";

            organizationFinder = dfService.Register(this, searchm, codeName, customerResultValue,
                  GetSearchConditionCollection,
                 delegate(object inputSource, object[] resultData)
                 {
                     if (resultData != null)
                     {
                         List<Guid> ids = new List<Guid>();
                         List<string> nos = new List<string>();

                         List<Guid> idList = new List<Guid>();
                         List<Guid> currencyList = new List<Guid>();

                         foreach (object item in resultData)
                         {
                             WFBusinessList wfb = item as WFBusinessList;
                             if (wfb != null)
                             {
                                 if (customerID == Guid.Empty)
                                 {
                                     customerID = wfb.CustomerID;
                                     customerName = wfb.CustomerName;
                                 }
                                 if (CompanyID == Guid.Empty)
                                 {
                                     CompanyID = wfb.CompanyID;
                                 }

                                 if (!ids.Contains(wfb.ID))
                                 {
                                     ids.Add(wfb.ID);
                                 }
                                 if (!nos.Contains(wfb.OperationNO))
                                 {
                                     nos.Add(wfb.OperationNO);
                                 }

                                 idList.Add(wfb.ID);
                                 currencyList.Add(wfb.CommissionCurrencyID);
                             }
                         }
                         string id = string.Empty;
                         string no = string.Empty;

                         foreach (Guid itemID in ids)
                         {
                             id = id + itemID.ToString() + ",";
                         }
                         foreach (string itemNo in nos)
                         {
                             no = no + itemNo + ",";
                         }


                         if (!string.IsNullOrEmpty(id))
                         {
                             this.Tag = id.Substring(0, id.Length - 1);
                             this.Text = no.Substring(0, no.Length - 1);
                         }

                         #region 填充表单数据

                         ComissionData comissData = wfExtendService.GetWFComissionDataByCodition(idList, currencyList, customerID, LocalData.IsEnglish);
                         if (comissData != null)
                         {
                             BindFormData(vParent, comissData, customerID, customerName);
                         }
                         #endregion

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

            item.AddWithValue("CompanyID", CompanyID,false);
            item.AddWithValue("CustomerID", customerID, false);
            item.AddWithValue("CustomerName", customerName, false);
            item.AddWithValue("SelectIDs", this.Tag, false);
            item.AddWithValue("SelectNos", this.Text, false);

            return item;
        }


        private void BindFormData(LWBaseForm vParent, ComissionData comissData, Guid customerID, string customerName)
        {
            DataSet ds = vParent.DataSource;
            if (ds != null && ds.Tables["MainTable"] != null)
            {
                DataTable dt = ds.Tables["MainTable"];
                if (dt.Rows.Count == 0)
                {
                    return;
                }
                dt.Rows[0]["OperactionIDs"] = this.Tag;
                dt.Rows[0]["OperationNo"] = this.Text;
                dt.Rows[0]["CustomerId"] = customerID;
                dt.Rows[0]["Customer"] = customerName;
                dt.Rows[0]["BLNO"] = comissData.BlNos;
                dt.Rows[0]["Profit"] = comissData.Profit;
                //dt.Rows[0]["CollectFreightCharges"] = comissData.Debit;
                //dt.Rows[0]["PaymentFreightCharges"] = comissData.Credit;
                dt.Rows[0]["CollectFreightCharges"] = comissData.Credit;
                dt.Rows[0]["PaymentFreightCharges"] = comissData.Debit;
                dt.Rows[0]["FreightType"] = comissData.PaymentType.ToString();
                dt.Rows[0]["IsPaid"] = comissData.IsPaid.ToString();
                dt.Rows[0]["Goods"] = comissData.Goods;

                if (!string.IsNullOrEmpty(comissData.CurrencyName))
                {
                    dt.Rows[0]["Currency"] = comissData.CurrencyName;
                }
                else
                {
                    dt.Rows[0]["Currency"] = "RMB";
                }
                dt.Rows[0]["PaymentCommission"] = comissData.CommissionAmount;
                if (!string.IsNullOrEmpty(comissData.Remark))
                {
                    comissData.Remark = comissData.Remark.Replace("&&", System.Environment.NewLine);
                }

                dt.Rows[0]["Remark"] = comissData.Remark;
                dt.Rows[0]["DepartmentID"] = comissData.CompanyID;

                vParent.DataSource = ds;
            }
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
        /// <summary>
        /// 目标控件名
        /// </summary>
        public string TargetControlName
        {
            get;
            set;
        }

        /// <summary>
        /// 目标控件的属性
        /// </summary>
        public string TargetControlProperty
        {
            get;
            set;
        }


        /// <summary>
        /// 目标控件绑定数据源信息
        /// </summary>
        public IBindingService TargetControl
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


            if (this.Value == null || this.Value == System.DBNull.Value) return;

            IWorkFlowExtendService extendService = null;
            LWBaseForm vParent = GetWFParentForm(this);
            if (vParent != null)
            {
                extendService = (IWorkFlowExtendService)vParent.ServiceContainer.Get(typeof(IWorkFlowExtendService));
            }

            if (extendService == null) return;


            if (vParent.DataSource != null && vParent.DataSource.Tables["MainTable"] != null && vParent.DataSource.Tables["MainTable"].Rows.Count > 0)
            {
                string id = vParent.DataSource.Tables["MainTable"].Rows[0]["OperactionIDs"] == null ? string.Empty : vParent.DataSource.Tables["MainTable"].Rows[0]["OperactionIDs"].ToString();
                if (!string.IsNullOrEmpty(id) && id != Guid.Empty.ToString())
                {
                    string[] idList = id.Split(',');

                    string operactionNos = extendService.GetCommissionBusinessNos(idList);

                    this.Text = operactionNos;
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
        [Browsable(true)]
        [ICPBrowsable(false)]
        [SRCategory("Data"),
        SRDisplayName("FindTextDisplayMember"),
        SRDescription("FindTextDisplayMember")]
        public DipalyMember DisplayMember
        {
            set { displayField = value; }
            get { return displayField; }
        }


        ValueMember valueField = ValueMember.Name;
        [DefaultValue("")]
        [Browsable(true)]
        [ICPBrowsable(false)]
        [SRCategory("Data"),
        SRDisplayName("FindTextValueMember"),
        SRDescription("FindTextValueMember")]
        public ValueMember ValueMember
        {
            set { valueField = value; }
            get { return valueField; }
        }
        [ICPBrowsable(false)]
        public Guid CustomerID
        {
            get;
            set;
        }

        [ICPBrowsable(false)]
        public bool IsCommission
        {
            get;
            set;
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

        WorkItem workitem = null;
        IWorkFlowExtendService wfExtendService;


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
                    if (val == null || string.IsNullOrEmpty(val.ToString()) ||
                        (Utility.IsGuid(val.ToString()) && Utility.GuidIsNullOrEmpty(new Guid(val.ToString()))))
                    {

                        string message = Utility.GetString("MustInput", "必须填写");
                        message = (LocalData.IsEnglish ? this.columnName : this.Caption) + " " + message;
                        errorTip.SetError(this, message);
                        errors.Add(message);
                        isSucc = false;
                    }
                }

                if (mainDataSource.Rows[0]["OperactionIDs"] == null)
                {
                    string message = Utility.GetString("Please select the Operaction no", "请选择业务");
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
        /// 验证(对于设计时)
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        public bool ValidateForDesign(List<string> errors)
        {
            return true;
        }
        #endregion

        #region 由其他控件关联绑定
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

                    CustomerID = new Guid(initDataObject.ToString());

                    ////不时邦定到bingdingsource的不用刷新数据源
                    //if (string.IsNullOrEmpty(DataSourceName)) return;
                    //if (this.DataSource == null) return;

                    ////在非WFForm窗体为父窗体的中不用刷新数据
                    //WFForm parent = (WFForm)this.GetWFParentForm(this);
                    //if (parent == null) return;

                    ////根据设置值刷新数据源
                    //IInitiDataService initSvc = parent.BindingSources[DataSourceName] as IInitiDataService;
                    //if (initSvc != null)
                    //{
                    //    Dictionary<string, object> vals = new Dictionary<string, object>();
                    //    vals.Add("InitDataId", value);
                    //    initSvc.InitData(parent.ServiceContainer, vals);

                    //    BindingSource bs = parent.BindingSources[DataSourceName] as BindingSource;
                    //    if (bs != null)
                    //    {
                    //        this.DataSource = bs.DataSource;
                    //    }
                    //}
                }
            }
        }
        #endregion



    }
}

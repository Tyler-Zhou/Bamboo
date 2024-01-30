using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Design;
using System.Windows.Forms;
using ICP.WF.ServiceInterface;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Attributes;
using System.Linq;
using ICP.Framework.CommonLibrary.Client;
using DevExpress.XtraEditors.Controls;
using ICP.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Server;

namespace ICP.WF.Controls
{
    /// <summary>
    /// 业务管理成本DataGridView列
    /// </summary>
    public class LWCostItemColumn : DataGridViewColumn, IInitiDataService, IColumn, IValidateService
    {
        #region 本地变量

        private string _cdisplayMember;
        private string _edisplayMember;
        private string _valueMember;
        private object _dataSource;
        private string _cfullDisplayMember;
        private string _efullDisplayMember;
        private object _initParentKey;
        private string _parentMember;
        private List<CostItemData> DataList = new List<CostItemData>();
        private IConfigureService _configureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }


        private Guid ManagementCostID = new Guid("8D92821D-D365-44E1-B854-FFA379E389F8");
        private Guid TransportCostID = new Guid("C1E11F58-D793-4E18-97E8-18220FCF3892");
        private Guid BusinessCostID = new Guid("A039834F-9094-4CAC-A3B6-AF5B43650622");

        #endregion

        #region 构造函数


        public LWCostItemColumn()
            : base(new DataGridViewTreeViewCell())
        {
            List<CostItemData> items = new List<CostItemData>();
            this.DataSource = items;
            this.ParentMember = "ParentIID";
            this.ValueMember = "ID";
            this.CDisplayMember = "CName";
            this.EDisplayMember = "EName";
            this.CFullDisplayMember = "FullName";
            this.EFullDisplayMember = "EFullName";

        }

        #endregion

        #region IInitiDataService接口实现

        public void InitData(IServiceContainerManager containerService, IDictionary<string, object> vars)
        {
            if (containerService == null) return;
            IWorkFlowExtendService extendService = (IWorkFlowExtendService)containerService.Get(typeof(IWorkFlowExtendService));
            if (extendService != null)
            {
                DataList = new List<CostItemData>();
                if (ParentNodeType == CostType.ManagementCost)
                {
                    //管理成本 直接取会计科目中的
                    DataList = extendService.GetCostItemList(LocalData.UserInfo.DefaultCompanyID);
                    if (DataList == null || DataList.Count == 0)
                    {
                        DataList = extendService.GetCostItemList(LocalData.UserInfo.DefaultCompanyID);
                    }
                    else
                    {
                        List<CostItemData> dbList = extendService.GetAllCostItemList();
                        try
                        {
                            foreach (KeyValuePair<string, object> var in vars)
                            {
                                if (var.Key == "申请表单->费用代码")
                                {
                                    string[] codestrArr = var.Value.ToString().Split(',');
                                    foreach (string codestr in codestrArr)
                                    {
                                        if (!string.IsNullOrEmpty(codestr))
                                        {
                                            string chargecode = codestr.Substring(codestr.IndexOf("\"") + 1, codestr.LastIndexOf("\"") - codestr.IndexOf("\"") - 1);
                                            if (DataList.Count(r => r.ID.ToString().ToUpper() == chargecode.ToUpper()) == 0)
                                            {
                                                Guid guid = new Guid(chargecode);
                                                if (guid != Guid.Empty)
                                                {
                                                    if (DataList.Count(r => r.ID == guid) == 0)
                                                    {
                                                        SolutionGLCodeList chargeCost = _configureService.GetSolutionGLCodeInfoNew(guid, LocalData.IsEnglish);

                                                        CostItemData cost = new CostItemData();
                                                        cost.ID = chargeCost.ID;
                                                        cost.ParentID = chargeCost.ParentID;
                                                        cost.CName = chargeCost.CName;
                                                        cost.EName = chargeCost.EName;
                                                        cost.EFullName = chargeCost.EfullName;
                                                        cost.FullName = chargeCost.FullName;
                                                        cost.GroupID = chargeCost.GroupByID;

                                                        DataList.Add(cost);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch
                        {

                        }
                    }

                    if (DataList == null || DataList.Count == 0)
                    {
                        DataList = extendService.GetCostItemList(LocalData.UserInfo.DefaultCompanyID);
                    }
                }
                else if (ParentNodeType == CostType.BusinessCost)
                {
                    //影视公司的生产成本 
                    DataList = extendService.GetMovieCostList();
                }
                else if (ParentNodeType == CostType.TransportCost)
                {
                    List<CostItemData> dbList = extendService.GetAllCostItemList();
                    //运输成本 
                    CostItemData tager = dbList.Find(delegate(CostItemData uItem) { return uItem.ID == TransportCostID; });
                    if (tager != null)
                    {
                        DataList.Add(tager);
                        BindCostItemTree(dbList, DataList, tager.ID);
                    }
                }

                if (DataList == null)
                {
                    DataList = new List<CostItemData>();
                }
                this.DataSource = DataList;
                this.ParentMember = "ParentID";
            }
            else
            {
                //没有服务
                List<CostItemData> items = new List<CostItemData>();
                this.DataSource = items;
                this.ParentMember = "ParentID";
            }
        }
        /// <summary>
        /// 生成树
        /// </summary>
        /// <param name="dbList">数据库中的数据</param>
        /// <param name="datalist">树结构源</param>
        /// <param name="id"></param>
        private void BindCostItemTree(List<CostItemData> dbList, List<CostItemData> datalist, Guid? id)
        {
            List<CostItemData> searchList = (from d in dbList where d.ParentID == id select d).ToList();

            if (searchList != null && searchList.Count > 0)
            {
                foreach (CostItemData item in searchList)
                {
                    datalist.Add(item);

                    BindCostItemTree(dbList, datalist, item.ID);
                }
            }

        }

        #endregion

        #region 数据邦定相关属性

        CostType parentNode = CostType.ManagementCost;
        [Browsable(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [SRCategory("Custom"), SRDescription("CostTypeDescription")]
        [ICP.Framework.CommonLibrary.Attributes.ICPBrowsable(true), SRDisplayName("ComboBoxType")]
        public CostType ParentNodeType
        {
            get { return parentNode; }
            set
            {
                parentNode = value;
            }
        }

        /// <summary>
        /// 显示成员
        /// </summary>
        [Browsable(true)]
        [SRCategory("DataSource"), SRDescription("CDisplayMember")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        public string CDisplayMember
        {
            get
            {
                return _cdisplayMember;
            }
            set
            {
                _cdisplayMember = value;
            }
        }

        /// <summary>
        /// 显示成员
        /// </summary>
        [Browsable(true)]
        [SRCategory("DataSource"), SRDescription("EDisplayMember")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        public string EDisplayMember
        {
            get
            {
                return _edisplayMember;
            }
            set
            {
                _edisplayMember = value;
            }
        }

        /// <summary>
        /// 单元格中显示成员
        /// </summary>
        [Browsable(true)]
        [SRCategory("DataSource"), SRDescription("FullDisplayMember")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        public string CFullDisplayMember
        {
            get
            {
                return _cfullDisplayMember;
            }
            set
            {
                _cfullDisplayMember = value;

            }
        }

        /// <summary>
        /// 单元格中显示成员
        /// </summary>
        [Browsable(true)]
        [SRCategory("DataSource"), SRDescription("EFullDisplayMember")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        public string EFullDisplayMember
        {
            get
            {
                return _efullDisplayMember;
            }
            set
            {
                _efullDisplayMember = value;

            }
        }


        /// <summary>
        /// 值成员

        /// </summary>
        [Browsable(true)]
        [SRCategory("DataSource"), SRDescription("ValueMember")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        public string ValueMember
        {
            get
            {
                return _valueMember;
            }
            set
            {
                if (value != null)
                {
                    _valueMember = value;
                }

            }
        }

        /// <summary>
        /// 父成员

        /// </summary>
        [Browsable(false)]
        public string ParentMember
        {
            get
            {
                return _parentMember;
            }
            set
            {
                _parentMember = value;

            }
        }


        [Browsable(false)]
        public object DataSource
        {
            get
            {
                return _dataSource;
            }
            set
            {
                if (value != null)
                {
                    if (value is DataTable
                        || value is DataSet
                        || value is DataView
                        || value is IList)
                    {
                        _dataSource = value;
                    }
                    else
                    {
                        throw new ApplicationException("不支持的数据源,数据源必须是DataTable,DataSet,DataView中的一种");
                    }
                }


            }
        }




        /// <summary>
        /// 根节点,父成员的值
        /// </summary>
        [Browsable(false)]
        public object InitParentKey
        {
            get
            {
                if (this.ParentNodeType == CostType.ManagementCost)
                {
                    this._initParentKey = ManagementCostID;
                }
                else if (ParentNodeType == CostType.TransportCost)
                {
                    this._initParentKey = TransportCostID;
                }
                else if (ParentNodeType == CostType.BusinessCost)
                {
                    this._initParentKey = BusinessCostID;
                }

                return _initParentKey;
            }
            set
            {
                _initParentKey = value;

            }
        }

        #endregion

        #region DataGridViewColumn方法属性重载


        private DataGridViewTreeViewCell TreeTextBoxCellTemplate
        {
            get
            {
                return (DataGridViewTreeViewCell)this.CellTemplate;
            }
        }

        public override object Clone()
        {
            LWCostItemColumn col = new LWCostItemColumn();
            //column1.CellTemplate = new DataGridViewTreeViewCell();
            col.CText = this.CText;
            col.EText = this.EText;
            col.HeaderText = this.HeaderText;
            col.DataPropertyName = this.DataPropertyName;
            col.CDisplayMember = this.CDisplayMember;
            col.EDisplayMember = this.EDisplayMember;
            col.ValueMember = this.ValueMember;
            col.ParentMember = this.ParentMember;
            col.InitParentKey = this.InitParentKey;
            col.DataSource = this.DataSource;
            col.CFullDisplayMember = this.CFullDisplayMember;
            col.EFullDisplayMember = this.EFullDisplayMember;
            col.Width = this.Width;
            col.Visible = this.Visible;
            col.ValueType = this.ValueType;
            col.ToolTipText = this.ToolTipText;
            col.ParentNodeType = this.ParentNodeType;
            col.Tag = this.Tag;
            col.Selected = this.Selected;
            col.Resizable = this.Resizable;
            col.ReadOnly = this.ReadOnly;
            col.Name = this.Name;
            col.MinimumWidth = this.MinimumWidth;
            col.MaxLength = this.MaxLength;
            col.DefaultHeaderCellType = this.DefaultHeaderCellType;
            col.DefaultCellStyle = this.DefaultCellStyle;
            col.ColumnType = this.ColumnType;
            col.AutoSizeMode = this.AutoSizeMode;

            col.AllowNull = this.AllowNull;
            return col;
        }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                if (value != null && !value.GetType().IsAssignableFrom(typeof(DataGridViewTreeViewCell)))
                {
                    throw new InvalidCastException("不是DataGridViewTreeViewCell");
                }
                base.CellTemplate = value;
            }
        }

        //[Browsable(true)]
        //public new string HeaderText
        //{
        //    get
        //    {
        //       return base.HeaderText;
        //    }
        //    set
        //    {
        //        base.HeaderText = value;
        //    }
        //}

        [Browsable(true)]
        [SRCategory("Custom")]
        public new string DataPropertyName
        {
            get
            {
                return base.DataPropertyName;
            }
            set
            {
                base.DataPropertyName = value;
            }
        }

        #endregion

        #region 自定义属性

        [Browsable(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [SRDisplayName("ReadOnly"), ICPBrowsable(true), SRCategory("Custom"), SRDescription("ReadOnly")]
        public bool ReadOnly
        {
            get { return base.ReadOnly; }
            set { base.ReadOnly = value; }
        }

        string cText;
        [Browsable(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [SRDisplayName("CText"), ICPBrowsable(true), SRCategory("Custom"), SRDescription("ChineseDescription")]
        public string CText
        {
            get { return cText; }
            set
            {
                cText = value;

                if (string.IsNullOrEmpty(eText))
                {
                    eText = value;
                }

                if (Utility.IsEnglish == false)
                {
                    base.HeaderText = cText;
                }
            }
        }


        string eText;
        [Browsable(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [SRDisplayName("EText"), ICPBrowsable(true), SRCategory("Custom"), SRDescription("EnglishDescription")]
        public string EText
        {
            get { return eText; }
            set
            {
                eText = value;
                if (string.IsNullOrEmpty(cText))
                {
                    cText = value;
                }

                if (Utility.IsEnglish)
                {
                    base.HeaderText = eText;
                }
            }
        }
        [Browsable(false)]
        public new string HeaderText
        {
            get
            {
                if (Utility.IsEnglish)
                {
                    if (string.IsNullOrEmpty(eText) == false)
                    {
                        return eText;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(cText) == false)
                    {
                        return cText;
                    }
                }

                return base.HeaderText;
            }
            set
            {
                base.HeaderText = value;
            }
        }

        [SRDisplayName("ColumnsWidth"), ICPBrowsable(true), SRCategory("Custom"), SRDescription("DescColumnsWidth")]
        public new int Width
        {
            get
            {
                return base.Width;
            }
            set
            {
                base.Width = value;
            }
        }

        #endregion

        #region IColumn接口成员

        FieldType _filedType = FieldType.Other;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(true), SRCategory("Custom"), SRDisplayName("DispFiledType"), ICPBrowsable(true), SRDescription("FiledType")]
        public FieldType FiledType
        {
            get { return _filedType; }
            set { _filedType = value; }
        }

        // string columnName;
        /// <summary>
        /// 列名 
        /// </summary>
        [Browsable(true)]
        [SRCategory("Custom"), SRDescription("ColumnName")]
        [SRDisplayName("DispColumnName"), ICPBrowsable(true)]
        public string ColumnName
        {
            get
            {

                return this.DataPropertyName;
            }
            set
            {
                this.DataPropertyName = value;
            }
        }


        /// <summary>
        /// 列类型

        /// </summary>
        Type columnType = typeof(string);
        [Browsable(false)]
        public Type ColumnType
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

                    System.Type type = typeof(CostItemData);
                    if (type != null)
                    {
                        System.Reflection.PropertyInfo pi = type.GetProperty(this.ValueMember);
                        if (pi != null)
                        {
                            return pi.PropertyType;
                        }
                    }
                }
                return typeof(string);
            }
            set { columnType = value; }
        }

        /// <summary>
        /// 最大长度

        /// </summary>
        [Browsable(false)]
        public int MaxLength { get; set; }


        bool allowNull = true;
        /// <summary>
        /// 是否允许为null
        /// </summary>
        [Browsable(true)]
        [SRCategory("Custom"), SRDescription("AllowNull")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [SRDisplayName("DispAllowNull"), ICPBrowsable(true)]
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

        /// <summary>
        /// 列标题

        /// </summary>
        [Browsable(false)]
        public string Caption
        {
            get
            {
                if (Utility.IsEnglish)
                {
                    return this.EText;
                }
                else
                {
                    return this.CText;
                }
            }
            set
            {
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [SRDisplayName("Name"), ICPBrowsable(true), SRCategory("Base")]
        public new string Name
        {
            get
            {
                return base.Name;
            }
            set
            {
                base.Name = value;
            }
        }
        #endregion

        #region IValidateService接口成员

        public bool ValidateForRuntime(System.Windows.Forms.ErrorProvider errorTip, List<string> errors)
        {
            bool isSucc = true;
            if (errors == null) errors = new List<string>();


            return isSucc;
        }


        public bool ValidateForDesign(List<string> errors)
        {
            if (errors == null) errors = new List<string>();
            bool isSucc = true;

            //不存在数据源的情况

            if (this.DataSource == null)
            {
                errors.Add(Utility.GetString("NecessaryToProperty", "Necessary to set up [DataSource] property", this.Name, "DataSource"));
                isSucc = false;
            }

            //通过邦定数据源的时候,必须是对应的属性都要设置

            if (this.DataSource != null)
            {
                if (string.IsNullOrEmpty(this.CDisplayMember))
                {
                    errors.Add(Utility.GetString("NecessaryToProperty", "Necessary to set up [CDisplayMember] property", this.Name, "CDisplayMember"));
                    isSucc = false;
                }

                if (string.IsNullOrEmpty(this.EDisplayMember))
                {
                    errors.Add(Utility.GetString("NecessaryToProperty", "Necessary to set up [EDisplayMember] property", this.Name, "EDisplayMember"));
                    isSucc = false;
                }

                if (string.IsNullOrEmpty(this.EFullDisplayMember))
                {
                    errors.Add(Utility.GetString("NecessaryToProperty", "Necessary to set up [EFullDisplayMember] property", this.Name, "EFullDisplayMember"));
                    isSucc = false;
                }

                if (string.IsNullOrEmpty(this.CFullDisplayMember))
                {
                    errors.Add(Utility.GetString("NecessaryToProperty", "Necessary to set up [CFullDisplayMember] property", this.Name, "CFullDisplayMember"));
                    isSucc = false;
                }
                if (string.IsNullOrEmpty(this.ValueMember))
                {
                    errors.Add(Utility.GetString("NecessaryToProperty", "Necessary to set up [ValueMember] property", this.Name, "ValueMember"));
                    isSucc = false;
                }

                if (string.IsNullOrEmpty(this.ParentMember))
                {
                    errors.Add(Utility.GetString("NecessaryToProperty", "Necessary to set up [ParentMember] property", this.Name, "ParentMember"));
                    isSucc = false;
                }
            }

            if (string.IsNullOrEmpty(this.CText))
            {
                errors.Add(Utility.GetString("NecessaryToProperty", "Necessary to set up [CText] property", this.Name, "CText"));
                isSucc = false;
            }

            if (string.IsNullOrEmpty(this.EText))
            {
                errors.Add(Utility.GetString("NecessaryToProperty", "Necessary to set up [EText] property", this.Name, "EText"));
                isSucc = false;
            }

            return isSucc;
        }

        #endregion
    }

    public enum CostType
    {
        /// <summary>
        /// 管理成本
        /// </summary>
        ManagementCost,
        /// <summary>
        /// 运输成本
        /// </summary>
        TransportCost,
        /// <summary>
        /// 业务成本
        /// </summary>
        BusinessCost,
        /// <summary>
        /// 全部
        /// </summary>
        All
    }


}

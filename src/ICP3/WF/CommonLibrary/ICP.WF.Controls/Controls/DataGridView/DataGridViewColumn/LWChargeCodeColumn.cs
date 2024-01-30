using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Design;
using System.Windows.Forms;
using ICP.WF.ServiceInterface;
using ICP.WF.ServiceInterface.DataObject;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Attributes;

namespace ICP.WF.Controls
{
    /// <summary>
    /// 业务费用项目DataGridView列
    /// </summary>
    public class LWChargeCodeColumn : DataGridViewColumn, IInitiDataService, IColumn,IValidateService
    {
        #region 本地变量

        private string _cdisplayMember;
        private string _edisplayMember;
        private string _valueMember;
        private object _dataSource;

        #endregion

        #region 构造函数

        public LWChargeCodeColumn()
            : base(new DataGridViewComBoxCell())
        {
            List<ChargingCodeList> items = new List<ChargingCodeList>();
            this.DataSource = items;

            _valueMember = "ID";
            _cdisplayMember= "CName";
            _edisplayMember = "EName";
            ColumnName = "ChargingCode";

        }       
        
        /// <summary>
        /// 单元格模版
        /// </summary>
        public override DataGridViewCell CellTemplate
        {
            get
            {

                return base.CellTemplate;
            }
            set
            {
                if (value != null && !value.GetType().IsAssignableFrom(typeof(DataGridViewComBoxCell)))
                {
                    throw new InvalidCastException("Must be a CalendarCell");
                }
                base.CellTemplate = value;
            }
        }
    
   

        #endregion

        #region IInitiDataService接口实现

        public void InitData(IServiceContainerManager containerService, IDictionary<string, object> vars)
        {
            if (containerService == null) return;
            IWorkFlowExtendService extendService = (IWorkFlowExtendService)containerService.Get(typeof(IWorkFlowExtendService));
            if (extendService != null)
            {
                //根据代理接口从服务获取数据
                this.DataSource = extendService.GetChargeCodeList(null);
            }
            else
            {
                List<ChargingCodeList> items = new List<ChargingCodeList>();
                this.DataSource = items;
            }
        }

        #endregion

        #region 数据邦定相关属性

        /// <summary>
        /// 显示成员
        /// </summary>
        [Browsable(true)]
        [SRCategory("DataSource"), SRDescription("CDisplayMember")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        [ICP.Framework.CommonLibrary.Attributes.ICPBrowsable(false),SRDisplayName("CDisplayMember")]
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
        [ICP.Framework.CommonLibrary.Attributes.ICPBrowsable(false), SRDisplayName("EDisplayMember")]
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
        /// 值成员
        /// </summary>
        [Browsable(true)]
        [SRCategory("DataSource"), SRDescription("ValueMember")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        [ICP.Framework.CommonLibrary.Attributes.ICPBrowsable(false), SRDisplayName("ValueMember")]
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
                    if (this.DataSource != null)
                    {
                        DataTable dt = DataTableConverter.ConvertToDataTable(this.DataSource);
                        if (dt != null)
                        {
                            dt.PrimaryKey = new DataColumn[] { dt.Columns[value.ToString()] };
                        }
                    }
                    _valueMember = value;
                }

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

                        if (!string.IsNullOrEmpty(this.ValueMember))
                        {
                            DataTable dt = DataTableConverter.ConvertToDataTable(value);
                            if (dt != null)
                            {
                                dt.PrimaryKey = new DataColumn[] { dt.Columns[this.ValueMember] };
                            }
                        }


                        _dataSource = value;
                    }
                    else
                    {
                        throw new ApplicationException("不支持的数据源,数据源必须是DataTable,DataSet,DataView中的一种");
                    }
                }


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
            LWChargeCodeColumn col = new LWChargeCodeColumn();
            col.CText = this.CText;
            col.EText = this.EText;
            col.HeaderText = this.HeaderText;
            col.DataPropertyName = this.DataPropertyName;
            col.CDisplayMember = this.CDisplayMember;
            col.EDisplayMember = this.EDisplayMember;
            col.ValueMember = this.ValueMember;
            col.DataSource = this.DataSource;
            col.Width = this.Width;
            col.Visible = this.Visible;
            col.ValueType = this.ValueType;
            col.ToolTipText = this.ToolTipText;
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

        [Browsable(true)]
        [SRCategory("Custom")]
        [ICP.Framework.CommonLibrary.Attributes.ICPBrowsable(false), SRDisplayName("DispDataPropertyName")]
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

                if (Utility.IsEnglish==false)
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
        /// 列类型        /// </summary>
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

                    System.Type type = null; //typeof(CostItemData);
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

         
            return isSucc;
        }


        /// <summary>
        /// 验证(对于设计时)
        /// </summary>
        /// <param name="errors"></param>
        /// <returns></returns>
        public bool ValidateForDesign(List<string> errors)
        {
            if (errors == null) errors = new List<string>();
            bool isSucc = true;

            //不存在数据源的情况
            if (this.DataSource==null)
            {
                errors.Add(Utility.GetString("NecessaryToProperty", "Necessary to set up [DataSource] property", this.Name, Utility.GetString("DataSource","DataSource")));
                isSucc = false;
            }

            //通过邦定数据源的时候,必须是对应的属性都要设置
            if (this.DataSource!=null)
            {
                if (string.IsNullOrEmpty(this.CDisplayMember))
                {
                    errors.Add(Utility.GetString("NecessaryToProperty", "Necessary to set up [CDisplayMember] property", this.Name,  Utility.GetString("CDisplayMember","CDisplayMember")));
                    isSucc = false;
                }

                if (string.IsNullOrEmpty(this.EDisplayMember))
                {
                    errors.Add(Utility.GetString("NecessaryToProperty", "Necessary to set up [EDisplayMember] property", this.Name,  Utility.GetString("EDisplayMember","EDisplayMember")));
                    isSucc = false;
                }

                //if (string.IsNullOrEmpty(this.EFullDisplayMember))
                //{
                //    errors.Add(Utility.GetString("NecessaryToProperty", "Necessary to set up [EFullDisplayMember] property", this.Name,  Utility.GetString("EFullDisplayMember","EFullDisplayMember")));
                //    isSucc = false;
                //}

                //if (string.IsNullOrEmpty(this.CFullDisplayMember))
                //{
                //    errors.Add(Utility.GetString("NecessaryToProperty", "Necessary to set up [CFullDisplayMember] property", this.Name,  Utility.GetString("CFullDisplayMember","CFullDisplayMember")));
                //    isSucc = false;
                //}
                if (string.IsNullOrEmpty(this.ValueMember))
                {
                    errors.Add(Utility.GetString("NecessaryToProperty", "Necessary to set up [ValueMember] property", this.Name,  Utility.GetString("ValueMember","ValueMember")));
                    isSucc = false;
                }

                //if (string.IsNullOrEmpty(this.ParentMember))
                //{
                //    errors.Add(Utility.GetString("NecessaryToProperty", "Necessary to set up [ParentMember] property", this.Name, Utility.GetString("ParentMember","ParentMember")));
                //    isSucc = false;
                //}
            }

            if (string.IsNullOrEmpty(this.CText))
            {
                errors.Add(Utility.GetString("NecessaryToProperty", "Necessary to set up [CText] property", this.Name, Utility.GetString("CText","CText")));
                isSucc = false;
            }

            if (string.IsNullOrEmpty(this.EText))
            {
                errors.Add(Utility.GetString("NecessaryToProperty", "Necessary to set up [EText] property", this.Name, Utility.GetString("EText","EText")));
                isSucc = false;
            }

            return isSucc;
        }

        #endregion
    }



}

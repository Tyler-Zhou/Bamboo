using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Design;
using System.Windows.Forms;
using ICP.WF.ServiceInterface;
using ICP.WF.ServiceInterface.DataObject;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Common.ServiceInterface.DataObjects;

namespace ICP.WF.Controls
{
    
    public class LWCurrencyColumn : DataGridViewColumn, IInitiDataService, IColumn, IValidateService
    {
        private string _cdisplayMember;
        private string _edisplayMember;
        private string _valueMember;
        private object _dataSource;

        public LWCurrencyColumn()
            : base(new DataGridViewComBoxCell())
        {
            List<CurrencyList> list = new List<CurrencyList>();
            this.DataSource = list;
            this.ValueMember = "Code";
            this.CDisplayMember = "Code";
            this.EDisplayMember = "Code";
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
                if (value != null && !value.GetType().IsAssignableFrom(typeof(LWComboxColumn)))
                {
                    throw new InvalidCastException("Must be a CalendarCell");
                }
                base.CellTemplate = value;
            }
        }


        #region IInitiDataService接口实现

        public void InitData(IServiceContainerManager containerService, IDictionary<string, object> vars)
        {
            if (containerService == null) return;
            IWorkFlowExtendService extendService = (IWorkFlowExtendService)containerService.Get(typeof(IWorkFlowExtendService));
            if (extendService != null)
            {
                this.DataSource =extendService.GetCurrencys();
            }
            else
            {
                this.DataSource =new List<CurrencyList>();
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
        [ICPBrowsable(true)]
        [Browsable(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [SRCategory("Custom"), SRDescription("ChineseDescription")]
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
        [ICPBrowsable(true)]
        [Browsable(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [SRCategory("Custom"), SRDescription("EnglishDescription")]
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
                    if (string.IsNullOrEmpty(EText) == false)
                    {
                        return EText;
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(CText) == false)
                    {
                        return CText;
                    }
                }

                return base.HeaderText;
            }
            set
            {
                base.HeaderText = value;
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
        [ICP.Framework.CommonLibrary.Attributes.ICPBrowsable(false), SRDisplayName("CDisplayMember")]
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

        #region IColumn接口成员

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

        FieldType _filedType = FieldType.Other;
        [ICPBrowsable(true), Browsable(true), SRCategory("Custom")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public FieldType FiledType
        {
            get { return _filedType; }
            set { _filedType = value; }
        }

        // string columnName;
        /// <summary>
        /// 列名 
        /// </summary>
        [ICPBrowsable(true)]
        [Browsable(true)]
        [SRCategory("Custom"), SRDescription("ColumnName")]
        public string ColumnName
        {
            get
            {
                //columnName = Utility.GetPascalProperty(this.Name);

                //if (string.IsNullOrEmpty(EText) == false && string.IsNullOrEmpty(columnName))
                //{
                //    if (ChineseToPY.ContainHanZi(EText))
                //    {
                //        columnName = ChineseToPY.Convert(EText);
                //    }
                //    else
                //    {
                //        columnName = EText.Trim().Replace(" ", "").Replace("   ", "").Replace("'", "").Replace("-", "");
                //    }


                //    columnName = columnName.Replace("(", "").Replace(")", "").Replace("（", "").Replace("）","");
                //    columnName = columnName.Replace("[", "").Replace("]", "");
                //    columnName = columnName.Replace("{", "").Replace("}", "");
                //}

                //this.DataPropertyName = columnName;
                //return columnName;

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
        [ICPBrowsable(true)]
        [Browsable(true)]
        [SRCategory("Custom"), SRDescription("AllowNull")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
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

        [ICPBrowsable(true)]
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

        #region IValidateService接口成员

        /// <summary>
        /// 保存任务时的验证
        /// </summary>
        /// <param name="errorTip"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        public bool ValidateForRuntime(System.Windows.Forms.ErrorProvider errorTip, List<string> errors)
        {
            bool isSucc = true;
            if (errors == null) errors = new List<string>();

            




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


                if (string.IsNullOrEmpty(this.ValueMember))
                {
                    errors.Add(Utility.GetString("NecessaryToProperty", "Necessary to set up [ValueMember] property", this.Name, "ValueMember"));
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

        public override object Clone()
        {

            LWCurrencyColumn col = new LWCurrencyColumn();
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

        
    }


}

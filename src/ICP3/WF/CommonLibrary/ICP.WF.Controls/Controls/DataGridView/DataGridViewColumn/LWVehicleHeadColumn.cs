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
using ICP.TMS.ServiceInterface;
using ICP.WF.Controls;

namespace ICP.WF.Controls
{
    /// <summary>
    /// 币种 
    /// </summary>
    public class LWVehicleHeadColumn : DataGridViewColumn, IInitiDataService, IColumn, IValidateService
    {
        public LWVehicleHeadColumn()
            : base(new DataGridViewComBoxCell())
        {
            this.DataSource = new List<TruckDataList>();
            this.ValueMember = "ID";
            this.CDisplayMember = "TruckNo";
            this.EDisplayMember = "TruckNo";
            this.ColumnName = "TruckNo";
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



        #region IInitiDataService接口实现

        public void InitData(IServiceContainerManager containerService, IDictionary<string, object> vars)
        {
            if (containerService == null) return;
            IWorkFlowExtendService extendService = (IWorkFlowExtendService)containerService.Get(typeof(IWorkFlowExtendService));
            if (extendService != null)
            {
                this.DataSource = extendService.GetTruckList();
            }
            else
            {
                this.DataSource = new List<TruckDataList>();
            }
        }

        #endregion

        #region 自定义属性



        string cText;
        [Browsable(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [SRCategory("Custom"), SRDescription("ChineseDescription")]
        [SRDisplayName("CText"), ICPBrowsable(true)]
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
        [SRCategory("Custom"), SRDescription("EnglishDescription")]
        [SRDisplayName("EText"), ICPBrowsable(true)]
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

        [SRDisplayName("HeaderText"), ICPBrowsable(false), SRCategory("Custom"), SRDescription("DispHeaderText")]
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

        #region 数据邦定相关属性


        /// <summary>
        /// 英文名称
        /// </summary>
        string edisplayMember;
        [SRCategory("DataSource"), SRDescription("EDisplayMember")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [Browsable(true)]
        [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        [ICP.Framework.CommonLibrary.Attributes.ICPBrowsable(false), SRDisplayName("EDisplayMember")]
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

        /// <summary>
        /// 中文名称
        /// </summary>
        string cdisplayMember;
        [SRCategory("DataSource"), SRDescription("CDisplayMember")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [Browsable(true)]
        [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        [ICP.Framework.CommonLibrary.Attributes.ICPBrowsable(false), SRDisplayName("CDisplayMember")]
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


        string member;
        /// <summary>
        /// 显示成员
        /// </summary>
        [Browsable(false)]
        [SRCategory("DataSource"), SRDescription("DisplayMember")]
        [Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DefaultValue("")]
        [TypeConverter("System.Windows.Forms.Design.DataMemberFieldConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        public string DisplayMember
        {
            get
            {

                if (Utility.IsEnglish)
                {
                    member = edisplayMember;
                }
                else
                {
                    member = cdisplayMember;
                }

                return member;
            }
            set
            {
                member = value;
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
        public new string ValueMember
        {
            get;
            set;
        }



        [Browsable(false)]
        [SRCategory("DataSource"), SRDescription("DataSource")]
        [DefaultValue("")]
        [AttributeProvider(typeof(IListSource))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public new object DataSource
        {
            get;
            set;
        }




        #endregion

        #region IColumn接口成员

        FieldType _filedType = FieldType.Other;
        [Browsable(true), SRCategory("Custom")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [SRDisplayName("DispFiledType"), ICPBrowsable(true), SRDescription("FiledType")]

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

        #region IValidateService接口成员

        /// <summary>
        /// 验证(设计时)
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


            if (this.DataSource == null)//&& this.Items.Count == 0)
            {
                errors.Add(Utility.GetString("NecessaryToProperty", "Necessary to set up [DataSource] property", this.Name, Utility.GetString("DataSource", "DataSource")));
                isSucc = false;
            }

            //通过邦定数据源的时候,必须是对应的属性都要设置
            if (this.DataSource != null)
            {
                //if (string.IsNullOrEmpty(this.CDisplayMember))
                //{
                //    errors.Add(Utility.GetString("NecessaryToProperty", "Necessary to set up [CDisplayMember] property", this.Name, Utility.GetString("CDisplayMember", "CDisplayMember")));
                //    isSucc = false;
                //}

                //if (string.IsNullOrEmpty(this.EDisplayMember))
                //{
                //    errors.Add(Utility.GetString("NecessaryToProperty", "Necessary to set up [EDisplayMember] property", this.Name, Utility.GetString("EDisplayMember", "EDisplayMember")));
                //    isSucc = false;
                //}


                if (string.IsNullOrEmpty(this.ValueMember))
                {
                    errors.Add(Utility.GetString("NecessaryToProperty", "Necessary to set up [ValueMember] property", this.Name, Utility.GetString("ValueMember", "ValueMember")));
                    isSucc = false;
                }
            }

            if (string.IsNullOrEmpty(this.CText))
            {
                errors.Add(Utility.GetString("NecessaryToProperty", "Necessary to set up [CText] property", this.Name, Utility.GetString("CText", "CText")));
                isSucc = false;
            }

            if (string.IsNullOrEmpty(this.EText))
            {
                errors.Add(Utility.GetString("NecessaryToProperty", "Necessary to set up [EText] property", this.Name, Utility.GetString("EText", "EText")));
                isSucc = false;
            }

            return isSucc;
        }

        #endregion

        public override object Clone()
        {

            LWVehicleHeadColumn col = new LWVehicleHeadColumn();
            col.CText = this.CText;
            col.EText = this.EText;
            col.HeaderText = this.HeaderText;
            col.DataPropertyName = this.DataPropertyName;
            col.DisplayMember = this.DisplayMember;
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
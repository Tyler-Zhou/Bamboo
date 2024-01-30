
//-----------------------------------------------------------------------
// <copyright file="LWCheckBox.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.Controls
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using DevExpress.Utils.Menu;
    using DevExpress.XtraEditors;
    using ICP.Framework.CommonLibrary.Attributes;
    using System.Windows.Forms;

    /// <summary>
    /// 复选按钮
    /// </summary>
    [ToolboxBitmap(typeof(BaseEdit), "Bitmaps256.CheckEdit.bmp")]
    [DefaultProperty("DataProperty"), SRDescription("CheckBoxDesc"), SRTitle("CheckBoxTitle")]
    [Serializable()]
    public class LWCheckBox : CheckEdit, IBindingService, IValidateService, IColumn
    {
        #region 属性
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
        [SRDisplayName("Name"), ICPBrowsable(true), SRDescription("Name"), SRCategory("Base")]
        public new string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }
        /// <summary>
        /// 布局
        /// </summary>
        [SRDisplayName("Dock"), ICPBrowsable(true), SRDescription("Dock")]
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
        /// 值
        /// </summary>
        [SRDisplayName("EditValue"), ICPBrowsable(false), SRDescription("EditValue"), SRCategory("DataBinding")]
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
        /// <summary>
        /// 字体
        /// </summary>
        [SRDisplayName("DispFont"), ICPBrowsable(true), SRCategory("Base"), SRDescription("Font")]
        public new Font Font
        {
            get { return base.Font; }
            set { base.Font = value; }
        }

        /// <summary>
        /// 文本
        /// </summary>
        [SRDisplayName("DispText"), ICPBrowsable(true), SRCategory("Base"), SRDescription("Text")]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }

        /// <summary>
        /// 字体颜色
        /// </summary>
        [SRDisplayName("DispForeColor"), ICPBrowsable(true), SRCategory("Base")]
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
        /// <summary>
        /// 停靠
        /// </summary>
        [SRDisplayName("Anchor"), ICPBrowsable(true)]
        public new AnchorStyles Anchor
        {
            get { return base.Anchor; }
            set { base.Anchor = value; }
        }
        /// <summary>
        /// 选择
        /// </summary>
        [SRDisplayName("DispChecked"), ICPBrowsable(true), SRCategory("Base")]
        public override bool Checked
        {
            get
            {
                return base.Checked;
            }
            set
            {
                base.Checked = value;
            }
        }

        #endregion

        #region IBindingService接口成员

        string _dataProperty;
        [Browsable(false)]
        //[RefreshProperties(RefreshProperties.All)]
        //[TypeConverter(typeof(DataPropertyBindingConverter))]
        //[SRCategory("DataBinding"),SRDescription("DataProperty")]
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

        string _controlProperty = "Checked";
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
                if (this.ControlProperty.Equals("Checked"))
                {
                    return typeof(bool);
                }
                else
                {
                    return typeof(object);
                }
            }
        }

        public string[] GetCanBindingControlProperty()
        {
            return new string[] { "Checked", "Tag" };
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
                try{
                this.mainDataSource = datasource as System.Data.DataTable;
                this.DataBindings.Add(ControlProperty, datasource, DataProperty);
               }
                catch { }
            }
        }
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
                if (mainDataSource.Columns[DataProperty].AllowDBNull == false)
                {
                    object val = this.GetType().GetProperty(this.ControlProperty).GetValue(this,null);
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

        #region 自定义属性


        string cText = string.Empty;
        [ICP.Framework.CommonLibrary.Attributes.ICPBrowsable(true), SRDisplayName("CText")]
        [RefreshProperties(RefreshProperties.Repaint)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [SRCategory("Base"), SRDescription("ChineseDescription")]
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

                if (Utility.IsEnglish)
                {
                    this.Text = eText;
                }
                else
                {
                    this.Text = cText;
                }
            }
        }


        string eText =string.Empty;
        [ICP.Framework.CommonLibrary.Attributes.ICPBrowsable(true), SRDisplayName("EText")]
        [RefreshProperties(RefreshProperties.Repaint)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [SRCategory("Base"), SRDescription("EnglishDescription")]
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
                    this.Text = eText;
                }
                else
                {
                    this.Text = cText;
                }
            }
        }

        

        #endregion

        #region IColumn接口成员


        FieldType _filedType = FieldType.Other;
        [Browsable(false), SRCategory("DataCustom")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public FieldType FiledType
        {
            get { return _filedType; }
            set { _filedType = value; }
        }


        string columnName=string.Empty;
        /// <summary>
        /// 列名 
        /// </summary>
        [ICP.Framework.CommonLibrary.Attributes.ICPBrowsable(true), 
        SRDisplayName("DispColumnName")]
        [SRCategory("DataCustom"), 
        SRDescription("ColumnName")]
        public string ColumnName
        {
            get
            {
                return BuildColumnName(columnName);
            }
            set
            {
                columnName = BuildColumnName(value);
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
                if (this.DataPropertyType == typeof(System.Guid) || this.DataPropertyType == typeof(System.Guid?))
                {
                    if (_name.EndsWith("DepartmentId") == false)
                    {
                        _name = _name + "DepartmentId";
                    }
                }
                else
                {
                    if (_name.EndsWith("DepartmentName") == false)
                    {
                        _name = _name + "DepartmentName";
                    }
                }
            }
            else if (this._filedType == FieldType.User)
            {
                if (this.DataPropertyType == typeof(System.Guid) || this.DataPropertyType == typeof(System.Guid?))
                {
                    if (_name.EndsWith("UserId") == false)
                    {
                        _name = _name + "UserId";
                    }
                }
                else
                {
                    if (_name.EndsWith("UserName") == false)
                    {
                        _name = _name + "UserName";
                    }
                }
            }
            else if (this._filedType == FieldType.Job)
            {
                if (this.DataPropertyType == typeof(System.Guid) || this.DataPropertyType == typeof(System.Guid?))
                {
                    if (_name.EndsWith("RoleId") == false)
                    {
                        _name = _name + "RoleId";
                    }
                }
                else
                {
                    if (_name.EndsWith("RoleName") == false)
                    {
                        _name = _name + "RoleName";
                    }
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
            get { return this.DataPropertyType; }
            set { columnType = value; }
        }


        int maxLength = 50;
        /// <summary>
        /// 最大长度
        /// </summary>
        [Browsable(false)]
        [SRCategory("DataCustom"), SRDescription("MaxLength")]
        public int MaxLength
        {
            get
            {

                return maxLength;
            }
            set
            {
                maxLength = value;
            }
        }

        bool allowNull = true;
        /// <summary>
        /// 是否允许为null
        /// </summary>
        [ICP.Framework.CommonLibrary.Attributes.ICPBrowsable(true), SRDisplayName("DispAllowNull")]
        [SRCategory("DataCustom"), SRDescription("AllowNull")]
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
        [ICP.Framework.CommonLibrary.Attributes.ICPBrowsable(true), SRDisplayName("DispCaption")]
        [SRCategory("DataCustom"), SRDescription("Caption")]
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

        [SRCategory("DataCustom"),ICP.Framework.CommonLibrary.Attributes.ICPBrowsable(true)]
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

        #endregion
    }
}

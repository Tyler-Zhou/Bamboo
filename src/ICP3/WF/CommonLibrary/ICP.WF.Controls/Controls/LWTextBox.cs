

//-----------------------------------------------------------------------
// <copyright file="LWTextBox.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.Controls
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using DevExpress.XtraEditors;
    using ICP.Framework.CommonLibrary.Attributes;
    using ICP.Framework.CommonLibrary.Client;

    /// <summary>
    /// 文本控件
    /// </summary>
    [ToolboxBitmap(typeof(BaseEdit), "Bitmaps256.TextEdit.bmp")]
    [DefaultProperty("DataProperty"), 
    SRDescription("TextBox"), 
    SRTitle("TextBox")]
    [Serializable()]
    public class LWTextBox : TextEdit
        , IBindingService
        , IValidateService
        , IColumn
    {

        #region 属性

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


        #endregion

        #region IBindingService接口实现

        string _dataProperty;
        /// <summary>
        /// 绑定数据源上的属性
        /// </summary>
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
        /// <summary>
        /// 绑定控件对应的属性
        /// </summary>
        [Browsable(true)]
        [RefreshProperties(RefreshProperties.All)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [TypeConverter(typeof(DataBindingControlPropertyConverter))]
        [SRDisplayName("ControlProperty"), ICPBrowsable(true), SRCategory("DataBinding"), SRDescription("ControlProperty")]
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

        /// <summary>
        /// 数据属性类型
        /// </summary>
        [Browsable(false)]
        public System.Type DataPropertyType
        {
            get
            {
                return typeof(string);
            }
        }

        /// <summary>
        /// 返回能绑定到数据源的控件属性
        /// </summary>
        /// <returns></returns>
        public string[] GetCanBindingControlProperty()
        {
            return new string[] { "Text", "Tag" };
        }

        private System.Data.DataTable mainDataSource;
        /// <summary>
        /// 绑定到数据源
        /// </summary>
        /// <param name="datasource"></param>
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
        }
        #endregion

        #region IValidateService接口实现
        /// <summary>
        /// 验证(运行时)
        /// </summary>
        /// <param name="errorTip">错误提示控件</param>
        /// <param name="errors">错误里表</param>
        /// <returns>通过验证返回true,没通过返回false</returns>
        public bool ValidateForRuntime( ErrorProvider errorTip,List<string> errors)
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
                        string message = Utility.GetString("MustInput", "必须填写");
                        message = (LocalData.IsEnglish ? this.columnName : this.Caption) + " " + message;

                        errorTip.SetError(this, message);
                        errors.Add(message);
                        isSucc = false;
                    }
                }

                ///UNDONE:数据类型格式验证

                ///UNDONE:长度验证
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


        /// <summary>
        /// 列类型
        /// </summary>
        Type columnType;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Type ColumnType
        {
            get { return this.DataPropertyType; }
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
    }
}

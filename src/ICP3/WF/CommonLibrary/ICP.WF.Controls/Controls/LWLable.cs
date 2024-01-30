

//-----------------------------------------------------------------------
// <copyright file="LWLable.cs" company="LongWin">
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

    /// <summary>
    /// 标签控件
    /// </summary>
    [ToolboxBitmap(typeof(BaseEdit), "Bitmaps256.Label.bmp")]
    [DefaultProperty("DataProperty"), SRDescription("LableDesc"), SRTitle("LableTitle")]
    [Serializable()]
    public class LWLable :LabelControl, IColumn, IValidateService, ITitle, IBindingService
    {
        public LWLable()
        {
            this.AutoSizeMode = LabelAutoSizeMode.None;
        }

        #region 属性
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
        string cText = "标题";
        [Browsable(true)]
        [RefreshProperties(RefreshProperties.All)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [SRDisplayName("CText"), ICPBrowsable(true), SRCategory("Base"), SRDescription("ChineseDescription")]
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
                    this.Text = cText;
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


        string eText = "Title";
        [Browsable(true)]
        [RefreshProperties(RefreshProperties.All)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [SRDisplayName("EText"), ICPBrowsable(true), SRCategory("Base"), SRDescription("EnglishDescription")]
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
            }
        }

        [Browsable(false)]
        public override string Text
        {
            get
            {
                if (Utility.IsEnglish)
                {
                    return eText;
                }
                else
                {
                    return cText;
                }
            }
            set
            {
                base.Text = value;
            }
        }



        [SRDisplayName("Name"), ICPBrowsable(true), SRCategory("Base")]
        public new string Name
        {
            get { return  base.Name; }
            set { base.Name = value;}
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
        /// 停靠
        /// </summary>
        [SRDisplayName("Anchor"), ICPBrowsable(true)]
        public new AnchorStyles Anchor
        {
            get { return base.Anchor; }
            set{base.Anchor=value;}
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

        #endregion

        #region IBindingService接口实现

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

        [Browsable(false)]
        public System.Type DataPropertyType
        {
            get
            {
                return typeof(string);
            }
        }


        public string[] GetCanBindingControlProperty()
        {
            return new string[] { "Text", "Tag" };
        }

        private System.Data.DataTable mainDataSource;
        public void Binding(object datasource)
        {
            this.DataBindings.Clear();
            if (datasource != null 
                && this.FiledType!= FieldType.None
                && string.IsNullOrEmpty(DataProperty) == false 
                && string.IsNullOrEmpty(ControlProperty) == false)
            {
                this.mainDataSource = datasource as System.Data.DataTable;
                this.DataBindings.Add(ControlProperty, datasource, DataProperty);
            }
        }
        #endregion

        #region IColumn接口成员

        /// <summary>
        /// 字段类型
        /// </summary>
        FieldType _filedType = FieldType.None;
        [Browsable(true), SRCategory("DataCustom"), SRDisplayName("DispFiledType"), ICPBrowsable(true), SRDescription("FiledType")]
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
        [SRDisplayName("DispColumnName"), ICPBrowsable(true)]
        [SRCategory("DataCustom"), SRDescription("ColumnName")]
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
        [SRDisplayName("DispLength"), ICPBrowsable(true), SRCategory("DataCustom"), SRDescription("MaxLength")]
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

        bool allowNull = true;
        /// <summary>
        /// 是否允许为null
        /// </summary>
        [Browsable(true)]
        [SRDisplayName("DispAllowNull"), ICPBrowsable(true), SRCategory("DataCustom"), SRDescription("DispAllowNull")]
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
        [SRDisplayName("DispCaption"), ICPBrowsable(true), SRCategory("DataCustom"), SRDescription("Caption")]
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


        #region IValidateService接口成员
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


            if (string.IsNullOrEmpty(this.cText) == false && this.cText.Equals("标题"))
            {
                errors.Add(Utility.GetString("NecessaryToProperty", "Necessary to set up [CText] property", this.Name, "CText"));
                isSucc = false;
            }

            if (string.IsNullOrEmpty(this.eText) == false && this.eText.Equals("Title"))
            {
                errors.Add(Utility.GetString("NecessaryToProperty", "Necessary to set up [EText] property", this.Name, "EText"));
                isSucc = false;
            }

            return isSucc;
        }

        #endregion

    }
}

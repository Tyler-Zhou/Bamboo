

//-----------------------------------------------------------------------
// <copyright file="LWDatePicker.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.WF.Controls
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using DevExpress.XtraEditors;
    using ICP.Framework.CommonLibrary.Attributes;
    using System.Windows.Forms;
    using DevExpress.XtraEditors.Repository;
    using DevExpress.XtraEditors.Controls;
    using DevExpress.Utils;
    using ICP.Framework.CommonLibrary.Client;

    /// <summary>
    /// 日期控件
    /// </summary>
    [ToolboxBitmap(typeof(DateEdit), "Bitmaps256.DateEdit.bmp")]
    [SRDescription("DatePickerDesc"), SRTitle("DatePickerTitle")]
    [Designer("DevExpress.XtraEditors.Design.DateEditDesigner, DevExpress.XtraEditors.v10.1.Design")]
    [Serializable()]
    public class LWDatePicker : DateEdit,IBindingService,IValidateService,IColumn
    {
        #region 重写时间控件 

        public LWDatePicker()
            : base()
        {
            //this.Properties.Buttons.Add(new EditorButton(ButtonPredefines.Combo));

            if (LocalData.IsEnglish)
            {
                base.Properties.DisplayFormat.FormatString = "MM/dd/yyyy";
                base.Properties.EditFormat.FormatString = "MM/dd/yyyy";
            }
            else
            {
                base.Properties.DisplayFormat.FormatString = "yyyy-MM-dd";
                base.Properties.EditFormat.FormatString = "yyyy-MM-dd";
            }
       
            base.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;

            base.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
        }


        #endregion

        [Description("Gets settings specific to the date editor."), Category(CategoryName.Properties), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new RepositoryItemDateEdit Properties { get { return base.Properties as RepositoryItemDateEdit; } }


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


        string _dataProperty;
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
        SRDescription("Dock")]
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
        [SRDisplayName("Anchor"), 
        ICPBrowsable(true)]
        public new AnchorStyles Anchor
        {
            get { return base.Anchor; }
            set { base.Anchor = value; }
        }
        /// <summary>
        /// 值
        /// </summary>
        [SRDisplayName("EditValue"), 
        ICPBrowsable(false), 
        SRDescription("EditValue"),
        SRCategory("Base")]
        public new object EditValue
        {
            get
            {
                return base.EditValue;
            }
            set
            {
                DateTime? dt=null;
                if (value != null)
                {
                    try
                    {
                        dt = Convert.ToDateTime(value);
                    }
                    catch
                    {
                        dt =null;
                    }
                }
                if (dt != null)
                {
                    base.EditValue = dt;
                }
                else
                {
                    base.EditValue = value;
                }
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
        /// TAG
        /// </summary>
        [SRDisplayName("Tag"),
        ICPBrowsable(false), 
        SRDescription("Tag"), 
        SRCategory("DataBinding")]
        public new object Tag
        {
            get { return base.Tag; }
            set { base.Tag = value; }
        }


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

        string _controlProperty = "EditValue";
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

  
        public string[] GetCanBindingControlProperty()
        {
            return new string[] { "Text", "EditValue", "Tag" };
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
                try
                {
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
            return true;
        }
        #endregion


        #region IColumn接口成员
        [Browsable(false)]
        public System.Type DataPropertyType
        {
            get
            {
                return typeof(DateTime);
            }
        }

        FieldType _filedType = FieldType.Other;
        [SRCategory("DataCustom"), 
        SRDisplayName("DispFiledType"), 
        ICPBrowsable(false), 
        SRDescription("FiledType"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public FieldType FiledType
        {
            get { return _filedType; }
            set { _filedType = value; }
        }


        string columnName=string.Empty;
        /// <summary>
        /// 列名 
        /// </summary>
        [Browsable(true)]
        [SRDisplayName("DispColumnName"),
        ICPBrowsable(true),
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


        int maxLength = 50;
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
        [Browsable(true)]
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

    public class LWRepositoryItemDateEdit : RepositoryItemDateEdit
    {
        [Description("Returns the collection of buttons in the current button editor.")]
        [Category("Behavior")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Localizable(true)]
        [RefreshProperties(RefreshProperties.All)]
        public override EditorButtonCollection Buttons
        {
            get
            {
                return base.Buttons;
            }
        }


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Gets the appearance settings used to paint the header panel in the dropdown calendar.")]
        [Category("Appearance")]
        public override AppearanceObject AppearanceDropDownHeader
        {
            get
            {
                return base.AppearanceDropDownHeader;
            }
        }
        
        [Category("Appearance")]
        [Description("Gets the appearance settings used to paint the highlighted header panel in the dropdown calendar.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override AppearanceObject AppearanceDropDownHeaderHighlight
        {
            get
            {
                return base.AppearanceDropDownHeaderHighlight;
            }
        }
        
        [Category("Appearance")]
        [Description("Gets the appearance settings used to paint the text within the dropdown calendar.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override AppearanceObject AppearanceDropDownHighlight
        {
            get
            {
                return base.AppearanceDropDownHighlight;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Provides appearance settings used to paint week numbers.")]
        [Category("Appearance")]
        public override AppearanceObject AppearanceWeekNumber
        {
            get
            {
                return base.AppearanceWeekNumber;
            }
        }
    }
}



//-----------------------------------------------------------------------
// <copyright file="LWNumericCalcEdit.cs" company="LongWin">
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
    using DevExpress.XtraEditors.Controls;
    using DevExpress.XtraEditors.Repository;
    using ICP.Framework.CommonLibrary.Attributes;
    using ICP.Framework.CommonLibrary.Client;
    using DevExpress.XtraEditors.Mask;

    /// <summary>
    /// 带计算器的数字框控件
    /// </summary>
    [ToolboxBitmap(typeof(BaseEdit), "Bitmaps256.CalcEdit.bmp")]
    [DefaultProperty("DataProperty"), SRDescription("NumericCalcEditDesc"), SRTitle("NumericCalcEditTitle")]
    [Serializable()]
    public class LWNumericCalcEdit : CalcEdit, IBindingService, IValidateService, IColumn
    {
        public LWNumericCalcEdit()
            : base()
        {
            //this.Properties.Buttons.Add(new EditorButton(ButtonPredefines.Combo));
        }


        [Description("Gets settings specific to the date editor."), Category(CategoryName.Properties), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new RepositoryItemCalcEdit Properties { get { return base.Properties as RepositoryItemCalcEdit; } }


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
        /// 掩码
        /// </summary>
        [Browsable(true)]
        [RefreshProperties(RefreshProperties.All)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [SRDisplayName("EditMask"), ICPBrowsable(true), SRCategory("Base"), SRDescription("EditMaskDesc")]
        public string EditMask
        {
            get { return base.Properties.EditMask; }
            set
            {
                base.Properties.EditMask = value;
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
        /// <summary>
        /// 布局
        /// </summary>
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
        /// 字体
        /// </summary>
        [SRDisplayName("DispFont"),
        ICPBrowsable(true), 
        SRCategory("Base"),
        SRDescription("Font")]
        public new Font Font
        {
            get { return base.Font; }
            set { base.Font = value; }
        }

        /// <summary>
        /// 字体颜色
        /// </summary>
        [SRDisplayName("DispForeColor"),
        ICPBrowsable(true),
        SRCategory("Base")]
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
                    object val = this.GetType().GetProperty(this.ControlProperty).GetValue(this, null);
                    if (val == null || string.IsNullOrEmpty(val.ToString()) || Convert.ToDecimal(val)<=0)
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


        FieldType _filedType = FieldType.Other;
        /// <summary>
        /// 列类型
        /// </summary>
        [SRCategory("DataCustom"), 
        SRDisplayName("DispFiledType"),
        SRDescription("FiledType")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public FieldType FiledType
        {
            get { return _filedType; }
            set { _filedType = value; }
        }

        //[SRDisplayName("DispValue"), 
        //ICPBrowsable(true)]
        //public override decimal Value
        //{
        //    get
        //    {
        //        return base.Value;
        //    }
        //    set
        //    {
        //        base.Value = value;
        //    }
        //}

        string columnName;
        /// <summary>
        /// 列名 
        /// </summary>
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


        /// <summary>
        /// 最大长度
        /// </summary>
        [SRDisplayName("DispLength"), 
        ICPBrowsable(false), 
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
        [Browsable(true)]
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
    }
}

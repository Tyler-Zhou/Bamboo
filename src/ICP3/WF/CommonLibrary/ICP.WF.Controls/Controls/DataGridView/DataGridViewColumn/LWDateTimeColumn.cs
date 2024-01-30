using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.WF.Controls
{
    /// <summary>
    /// 时间列
    /// </summary>
    public class LWDateTimeColumn : DataGridViewColumn, IColumn,IValidateService
    {
        public LWDateTimeColumn()
            : base(new CalendarCell())
        {
            
        }


        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                if (value != null &&
                    !value.GetType().IsAssignableFrom(typeof(CalendarCell)))
                {
                    throw new InvalidCastException("Must be a CalendarCell");
                }
                base.CellTemplate = value;
            }
        }


        #region 自定义属性

        string cText;
        /// <summary>
        /// 中文名称
        /// </summary>
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

                if (Utility.IsEnglish==false)
                {
                    base.HeaderText = cText;
                }
            }
        }


        string eText;
        /// <summary>
        /// 英文名称
        /// </summary>
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

        public override object Clone()
        {
            LWDateTimeColumn col = new LWDateTimeColumn();
            col.CText = this.CText;
            col.EText = this.EText;
            col.HeaderText = this.HeaderText;
            col.DataPropertyName = this.DataPropertyName;

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
        Type columnType = typeof(DateTime);
        [Browsable(false)]
        public Type ColumnType
        {
            get { return columnType; }
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
        /// 列标题        /// </summary>
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

    public class CalendarCell : DataGridViewTextBoxCell
    {

        public CalendarCell()
            : base()
        {
            if (LocalData.IsEnglish)
            {
                this.Style.Format = "MM/dd/yyyy";
            }
            else
            {
                this.Style.Format = "yyyy-MM-dd";
            }
           
        }

        public override void InitializeEditingControl(int rowIndex, object
            initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {

            base.InitializeEditingControl(rowIndex, initialFormattedValue,
                dataGridViewCellStyle);
            CalendarEditingControl ctl = DataGridView.EditingControl as CalendarEditingControl;
            try
            {
                if (LocalData.IsEnglish)
                {
                    ctl.EditValue = Convert.ToDateTime(((DateTime)this.Value).ToString("MM/dd/yyyy"));
                }
                else
                {
                    ctl.EditValue = Convert.ToDateTime(((DateTime)this.Value).ToString("yyyy-MM-dd"));
                }
            }
            catch
            {
                if (LocalData.IsEnglish)
                {
                    ctl.EditValue = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"));
                }
                else
                {

                    ctl.EditValue = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                }
            }
        }

        public override Type EditType
        {
            get
            {
                return typeof(CalendarEditingControl);
            }
        }

        public override Type ValueType
        {
            get
            {
                return typeof(System.Nullable<DateTime>);
            }
        }

        public override object DefaultNewRowValue
        {
            get
            {
                return null;
            }
        }
    }

    class CalendarEditingControl : LWDatePicker, IDataGridViewEditingControl
    {
        DataGridView dataGridView;
        private bool valueChanged = false;
        int rowIndex;

        public CalendarEditingControl()
        {
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

        public object EditingControlFormattedValue
        {
            get
            {
                if (EditValue != null)
                {
                    if (LocalData.IsEnglish)
                    {
                        return Convert.ToDateTime(this.EditValue).ToString("MM/dd/yyyy");
                    }
                    else
                    {
                        return Convert.ToDateTime(this.EditValue).ToString("yyyy-MM-dd");
                    }
                }
                else
                {
                    return null;
                }

            }
            set
            {
                if (value is String)
                {
                    this.EditValue = DateTime.Parse((String)value);
                }
            }
        }

        public object GetEditingControlFormattedValue(
            DataGridViewDataErrorContexts context)
        {
            return EditingControlFormattedValue;
        }

        public void ApplyCellStyleToEditingControl(
            DataGridViewCellStyle dataGridViewCellStyle)
        {
            this.Font = dataGridViewCellStyle.Font;
        }

        public int EditingControlRowIndex
        {
            get
            {
                return rowIndex;
            }
            set
            {
                rowIndex = value;
            }
        }

        public bool EditingControlWantsInputKey(
            Keys key, bool dataGridViewWantsInputKey)
        {
            switch (key & Keys.KeyCode)
            {
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                case Keys.Right:
                case Keys.Home:
                case Keys.End:
                case Keys.PageDown:
                case Keys.PageUp:
                    return true;
                default:
                    return false;
            }
        }

        public void PrepareEditingControlForEdit(bool selectAll)
        {
        }

        public bool RepositionEditingControlOnValueChange
        {
            get
            {
                return false;
            }
        }

        public DataGridView EditingControlDataGridView
        {
            get
            {
                return dataGridView;
            }
            set
            {
                dataGridView = value;
            }
        }

        public bool EditingControlValueChanged
        {
            get
            {
                return valueChanged;
            }
            set
            {
                valueChanged = value;
            }
        }

        public Cursor EditingPanelCursor
        {
            get
            {
                return base.Cursor;
            }
        }

        protected override void OnEditValueChanged()
        {
            valueChanged = true;
            this.EditingControlDataGridView.NotifyCurrentCellDirty(true);

            base.OnEditValueChanged();
        }



    }
}

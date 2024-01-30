using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using ICP.Framework.CommonLibrary.Attributes;
using System.Drawing;
using ICP.FAM.ServiceInterface;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface;

namespace ICP.WF.Controls
{
    /// <summary>
    /// 搜索器编辑列
    /// </summary>
    public class LWFindTextColumn : DataGridViewColumn, IColumn, IValidateService
    {
        #region 构造函数

        public LWFindTextColumn()
            : base(new DataGridViewTextBoxCell())
        {
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
                    this.HeaderText = cText;
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
            LWFindTextColumn col = new LWFindTextColumn();
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

        private object _value;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        #endregion

        #region IColumn 成员

        FieldType _filedType = FieldType.GLCode;
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
            get { return columnType; }
            set { columnType = value; }
        }

        /// <summary>
        /// 最大长度
        /// </summary>
        int maxLength;
        [Browsable(false)]
        public int MaxLength
        {
            get
            {
                return this.maxLength;
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
        [SRCategory("Custom")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [SRDisplayName("DispAllowNull"), ICPBrowsable(true), SRDescription("DispAllowNull")]
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

        [Browsable(false)]
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
    }
}

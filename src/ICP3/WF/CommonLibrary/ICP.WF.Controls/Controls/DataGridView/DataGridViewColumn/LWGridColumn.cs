using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Attributes;

namespace ICP.WF.Controls
{

    [Serializable()]
    public class LWGridColumn : DevExpress.XtraGrid.Columns.GridColumn, IColumn, IValidateService
    {
        #region IColumn接口成员

        GridColumnStyle _columnStyle = GridColumnStyle.Text;
        /// <summary>
        /// 字段类型
        /// </summary>
        [ICPBrowsable(true),
        SRCategory("DataCustom"),
        SRDisplayName("DispFiledType"),
        SRDescription("FiledType")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public GridColumnStyle ColumnStyle
        {
            get { return _columnStyle; }
            set { _columnStyle = value; }
        }


        FieldType _filedType = FieldType.Other;
        /// <summary>
        /// 字段类型
        /// </summary>
        [ICPBrowsable(false),
        SRCategory("DataCustom"),
        SRDisplayName("DispFiledType"),
        SRDescription("FiledType")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public FieldType FiledType
        {
            get { return _filedType; }
            set { _filedType = value; }
        }


        string columnName = string.Empty;
        /// <summary>
        /// 列名 
        /// </summary>
        [ICPBrowsable(true)]
        [SRCategory("DataCustom"),
        SRDescription("ColumnName"),
        SRDisplayName("DispColumnName")]
        public string ColumnName
        {
            get
            {
                if (this.DesignMode)
                {
                    columnName = Utility.BuildSpecialColumnName(
                       columnName,
                        typeof(string),
                       this.FiledType);
                }
                return columnName;
            }
            set
            {
                if (this.DesignMode)
                {
                    columnName = Utility.BuildSpecialColumnName(
                        value,
                        typeof(string),
                        this.FiledType);

                    if (string.IsNullOrEmpty(this.Caption))
                    {
                        this.Caption = value;
                    }
                }
                else
                {
                    columnName = value;
                }

                this.Name = columnName;
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
            get
            {
                if (this.DesignMode)
                {
                    return typeof(string);
                }
                else
                {
                    return columnType;
                }
            }
            set
            {
                columnType = value;
            }
        }

        int maxLength = 50;
        /// <summary>
        /// 最大长度
        /// </summary>
        [ICPBrowsable(true)]
        [SRCategory("DataCustom"),
        SRDescription("MaxLength"),
        SRDisplayName("DispLength")]
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

        private bool allowNull = true;
        /// <summary>
        /// 是否允许为null
        /// </summary>
        [ICPBrowsable(true)]
        [SRCategory("DataCustom"),
        SRDescription("AllowNull"),
        SRDisplayName("DispAllowNull")]
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
        [ICPBrowsable(true)]
        [SRCategory("DataCustom"),
        SRDescription("Caption"),
        SRDisplayName("Caption")]
        public new string Caption
        {
            get
            {
                return base.Caption;
            }
            set
            {
                base.Caption = value;
            }
        }
        #endregion

        #region IValidateService接口成员

        public bool ValidateForRuntime(System.Windows.Forms.ErrorProvider errorTip, List<string> errors)
        {
            bool isSucc = true;
            if (errors == null) errors = new List<string>();


            return isSucc;
        }


        public bool ValidateForDesign(List<string> errors)
        {
            //if (errors == null) errors = new List<string>();
            //bool isSucc = true;

            //if (string.IsNullOrEmpty(this.CText))
            //{
            //    errors.Add(Utility.GetString("NecessaryToProperty", "Necessary to set up [CText] property", this.Name, "CText"));
            //    isSucc = false;
            //}

            //if (string.IsNullOrEmpty(this.EText))
            //{
            //    errors.Add(Utility.GetString("NecessaryToProperty", "Necessary to set up [EText] property", this.Name, "EText"));
            //    isSucc = false;
            //}

            //return isSucc;
            return true;
        }

        #endregion

    }


    public enum GridColumnStyle
    {
        Decimal,
        Text,
        DateTime,
        ChargeCode,
        User,
        Job,
        Organization

    }
}

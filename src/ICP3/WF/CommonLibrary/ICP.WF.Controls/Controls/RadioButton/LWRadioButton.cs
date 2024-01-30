using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Attributes;

namespace ICP.WF.Controls
{
    [ToolboxBitmap(typeof(LWRadioButton), "Resources.RadioButton.bmp")]
    [DefaultProperty("DataProperty"), SRDescription("RadioButton"), SRTitle("RadioButton")]
    [Serializable()]
    public class LWRadioButton : RadioButton, IComponent, IDisposable, IBindingService, IColumn
    {

        #region 创建唯一名(由于在在单选组控件中。该控件生成时候不能调用命名服务。所以先用该方法替代)
        [ThreadStatic]
        internal static int num = 0;

        private static string CreateUniqueName()
        {
            num++;
            return "radioItem" + num.ToString();
        }


        #endregion


        #region 构造函数



        public LWRadioButton()
            : base()
        {
            this.AutoSize = true;
        }
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
        protected override void OnCreateControl()
        {
            if (string.IsNullOrEmpty(this.Name))
            {
                this.Name = CreateUniqueName();
            }

            //防止重复
            if (string.IsNullOrEmpty(this.Name) == false)
            {
                try
                {
                    string endChar = this.Name.Substring(this.Name.Length - 1, 1);
                    int endNum = Convert.ToInt32(endChar);
                    if (endNum > num) num = endNum;
                }
                catch
                {
                }
            }

            base.OnCreateControl();
        }
        #endregion


        #region IBindingService接口成员

        string _dataProperty;
        [Browsable(false)]
        //[RefreshProperties(RefreshProperties.All)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        //[TypeConverter(typeof(DataPropertyBindingConverter))]
        //[SRCategory("DataBinding"), SRDescription("DataProperty")]
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
        [ICPBrowsable(true)]
        [RefreshProperties(RefreshProperties.All)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
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
                this.mainDataSource = datasource as System.Data.DataTable;
                this.DataBindings.Add(ControlProperty, datasource, DataProperty);
            }
        }
        #endregion

        #region 自定义属性


        string cText = "中文标题";
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


        string eText = "English Tile";
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
                    this.Text = eText;
                }
                else
                {
                    this.Text = cText;
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

        #endregion

        #region IColumn接口成员

        [ICPBrowsable(true)]
        FieldType _filedType = FieldType.None;
        [Browsable(true), SRCategory("Custom")]
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
        [ICPBrowsable(true)]
        [Browsable(true)]
        [SRCategory("Custom"), SRDescription("ColumnName")]
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
            }
        }


        /// <summary>
        /// 列类型

        /// </summary>
        Type columnType;
        [ICPBrowsable(true)]
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
        [ICPBrowsable(true)]
        [Browsable(true)]
        [SRCategory("Custom"), SRDescription("MaxLength")]
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
        [ICPBrowsable(true)]
        [Browsable(true)]
        [SRCategory("Custom"), SRDescription("AllowNull")]
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
        [ICPBrowsable(true)]
        [Browsable(true)]
        [SRCategory("Custom"), SRDescription("Caption")]
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

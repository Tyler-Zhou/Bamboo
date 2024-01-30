using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ICP.Framework.CommonLibrary.Attributes;

namespace ICP.WF.Controls
{
    [ToolboxBitmap(typeof(BaseEdit), "Bitmaps256.RadioGroup.bmp")]
    [DefaultProperty("DataProperty"), SRDescription("RadioGroup"), SRTitle("RadioGroup")]
    [Serializable()]
    public class LWRadioGroup : FlowLayoutPanel, IComponent, IDisposable, IBindingService, IValidateService, IColumn
    {
        #region 初始化


        public LWRadioGroup()
        {
            if (this.DesignMode)
            {
                InitializeComponent();
            }
        }


        protected override void OnControlAdded(ControlEventArgs e)
        {
            if (this.DesignMode)
            {
                e.Control.Tag = (this.Controls.Count - 1).ToString();
            }
            base.OnControlAdded(e);
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();

            AddRadio("同意", true);
            AddRadio("不同意", false);

            this.ResumeLayout(false);
        }

        #endregion

        #region 公共方法

        public void AddRadio(string text)
        {
            if (this.Controls.ContainsKey(text) == false)
            {
                LWRadioButton radiobutton = new LWRadioButton();
                radiobutton.Text = text;
                radiobutton.Tag = this.Controls.Count;
                radiobutton.AutoSize = true;
                this.Controls.Add(radiobutton);
            }
        }

        public void AddRadio(string text, object value)
        {
            if (this.Controls.ContainsKey(text) == false)
            {
                LWRadioButton radiobutton = new LWRadioButton();
                radiobutton.Text = text;
                radiobutton.Tag = value;
                radiobutton.AutoSize = true;
                this.Controls.Add(radiobutton);
            }
        }

        public string[] GetTitles()
        {
            List<string> texts = new List<string>();
            foreach (LWRadioButton rd in this.Items)
            {
                texts.Add(rd.Text);
            }

            return texts.ToArray();
        }
        #endregion


        #region IValidateService接口实现
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
                    if (val == null || string.IsNullOrEmpty(val.ToString()))
                    {
                        errorTip.SetError(this, Utility.GetString("MustInput", "必须填写！"));
                        errors.Add(Utility.GetString("MustInput", "必须填写！"));
                        isSucc = false;
                    }
                }

                //数据类型格式验证


                //长度验证
            }

            return isSucc;
        }

        public bool ValidateForDesign(List<string> errors)
        {
            if (errors == null) errors = new List<string>();
            bool isSucc = true;

            //不存在数据源的情况

            if (this.Items == null || this.Items.Count == 0)
            {
                errors.Add(Utility.GetString("NecessaryToProperty", "Necessary to set up [Items] property", this.Name, "Items"));
                isSucc = false;
            }

            return isSucc;
        }
        #endregion


        #region 公共属性
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
        [ICPBrowsable(true)]
        [SRCategory("Custom"), SRDescription("SelectedText")]
        [TypeConverter(typeof(RadionGroupSelectedTextConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string SelectedText
        {
            get
            {
                foreach (LWRadioButton child in this.Items)
                {
                    if (child.Checked == true)
                        return child.Text;
                }
                return string.Empty;
            }
            set
            {
                foreach (LWRadioButton child in this.Items)
                {
                    if (value != null && child.Text == value)
                    {
                        child.Checked = true;
                    }
                }
            }
        }

        [ICPBrowsable(true)]
        [SRCategory("Custom"), Description("Items")]
        [RefreshProperties(RefreshProperties.All)]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public RedioControlCollection Items
        {
            get
            {
                return (RedioControlCollection)this.Controls;
            }
        }


        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public object SelectedValue
        {
            get
            {
                foreach (LWRadioButton child in this.Items)
                {
                    if (child.Checked == true)
                        return child.Tag;
                }
                return null;
            }
            set
            {
                foreach (LWRadioButton child in this.Items)
                {
                    if (value != null && child.Tag.Equals(value) == true)
                    {
                        child.Checked = true;
                    }
                }
            }
        }
        #endregion


        #region 自定义属性

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


        string _dataProperty;
        //[Category("属性绑定")]
        [Browsable(false)]
        //[RefreshProperties(RefreshProperties.All)]
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        //[TypeConverter(typeof(DataPropertyBindingConverter))]
        //[Description("绑定数据源对应的属性.")]
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

        string _controlProperty = "SelectedValue";
        [Browsable(true)]
        [ICPBrowsable(true)]
        [RefreshProperties(RefreshProperties.All)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [TypeConverter(typeof(DataBindingControlPropertyConverter))]
        [SRCategory("DataBinding"), SRDescription("ControlProperty.")]
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
            return new string[] { "SelectedValue", "SelectedText" };
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

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected override Control.ControlCollection CreateControlsInstance()
        {
            return new RedioControlCollection(this);
        }




        #region IColumn接口成员


        FieldType _filedType = FieldType.Other;
        [ICPBrowsable(true)]
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


    [ListBindable(false), ComVisible(false)]
    public class RedioControlCollection : Control.ControlCollection
    {
        private LWRadioGroup _container;

        public RedioControlCollection(LWRadioGroup container)
            : base(container)
        {
            this._container = container;
        }

        public virtual void Add(LWRadioGroup control)
        {

            base.Add(control);
        }

        public virtual void AddRange(LWRadioGroup[] controls)
        {
            base.AddRange(controls);
        }



        public LWRadioGroup Container
        {
            get
            {
                return this._container;
            }
        }

        public new LWRadioButton this[int index]
        {
            get
            {
                return base[index] as LWRadioButton;
            }
        }

        public bool Contains(LWRadioButton control)
        {
            return base.Contains(control);
        }


        public int GetChildIndex(LWRadioButton child)
        {
            return base.GetChildIndex(child);
        }

        public virtual int GetChildIndex(LWRadioButton child, bool throwException)
        {
            return base.GetChildIndex(child, throwException);
        }

        public override IEnumerator GetEnumerator()
        {
            return base.GetEnumerator();
        }


        public virtual void Remove(LWRadioButton value)
        {
            base.Remove(value);
        }

        public virtual void SetChildIndex(LWRadioButton child, int newIndex)
        {
            base.SetChildIndex(child, newIndex);
        }
    }
}

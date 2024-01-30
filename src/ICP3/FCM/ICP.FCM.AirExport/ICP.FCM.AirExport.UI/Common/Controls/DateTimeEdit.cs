using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ICP.FCM.AirExport.UI.Common.Controls
{
    public partial class DateTimeEdit : UserControl
    {   
        
        #region init
        /// <summary>
        /// 日期时间控件
        /// </summary>
        public DateTimeEdit()
        {
            InitializeComponent();
            dateEdit1.Enter += delegate { if (Enter != null)Enter(this, EventArgs.Empty); };            
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            dateEdit1.Width = (Width - 75) >= 0 ? Width - 75 : 0;
            dateEdit1.EditValueChanged += new EventHandler(dateTime_EditValueChanged);
        }

        void dateTime_EditValueChanged(object sender, EventArgs e)
        {
            //if (this.DataBindings["EditValue"] != null)
            //{
            //    this.DataBindings["EditValue"].WriteValue();
            //}

            EditValue = dateEdit1.DateTime;
        }

        /// <summary>
        /// 禁止修改高度
        /// </summary>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Height = 21;
            dateEdit1.Width = (Width - 75) >= 0 ? Width - 75 : 0;
        }

        /// <summary>
        /// 背景色
        /// </summary>
        public override Color BackColor { get { return dateEdit1.BackColor; } set { dateEdit1.BackColor =  value; } }

        /// <summary>
        /// 有效性
        /// </summary>
        public new bool Enabled { get { return dateEdit1.Enabled; } set { dateEdit1.Enabled =  value; } }

        /// <summary>
        /// 只读
        /// </summary>
        public bool ReadOnly { get { return dateEdit1.Properties.ReadOnly; } set { dateEdit1.Properties.ReadOnly = value; } }


        public new event EventHandler Enter;
        public event CancelEventHandler EditValueChanging;
        public event EventHandler EditValueChanged;

        [Bindable(true)]
        public DateTime? EditValue
        {
            get
            {
                return dateEdit1.DateTime;
            }
            set
            {
                bool canChange = true;
                if (EditValueChanging != null)
                {
                    EditValueChanging(this, new CancelEventArgs(canChange));
                }

                dateEdit1.EditValueChanged -= new EventHandler(dateTime_EditValueChanged);

                if (canChange == false) return;

                if (value == null || !value.HasValue || value.Value.Year == 1)
                {
                    dateEdit1.EditValue = null;
                }
                else
                {
                    dateEdit1.DateTime = value.Value;
                }

                if (DataBindings["EditValue"] != null)
                {
                    DataBindings["EditValue"].WriteValue();
                }
                dateEdit1.EditValueChanged += new EventHandler(dateTime_EditValueChanged);

                if (EditValueChanged != null)
                {
                    EditValueChanged(this, EventArgs.Empty);
                }
            }
        }

        public Color SpecifiedBackColor
        {
            get
            {
                return dateEdit1.BackColor;
            }
            set
            {
                dateEdit1.BackColor = value;
            }
        }

        string _dateFormat = "yyyy-MM-dd";

        /// <summary>
        /// 日期格式
        /// 默认值：2000-01-01
        /// </summary>
        public string DateFormat
        {
            get
            {
                return _dateFormat;
            }
            set
            {
                _dateFormat = value;
            }
        }

        string _timeFormat = "HH:mm:ss";

        /// <summary>
        /// 时间格式
        /// 默认值：13:25:38
        /// </summary>
        public string TimeFormat
        {
            get
            {
                return _timeFormat;
            }
            set
            {
                _timeFormat = value;
            }
        }

        #endregion
    }
}

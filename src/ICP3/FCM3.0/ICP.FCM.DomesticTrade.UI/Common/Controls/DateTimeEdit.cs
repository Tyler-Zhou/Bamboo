using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ICP.FCM.DomesticTrade.UI.Common.Controls
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
            this.dateEdit1.Enter += delegate { if (this.Enter != null)this.Enter(this, EventArgs.Empty); };            
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.dateEdit1.Width = (this.Width - 75) >= 0 ? this.Width - 75 : 0;
            this.dateEdit1.EditValueChanged += new EventHandler(dateTime_EditValueChanged);
        }

        void dateTime_EditValueChanged(object sender, EventArgs e)
        {
            //if (this.DataBindings["EditValue"] != null)
            //{
            //    this.DataBindings["EditValue"].WriteValue();
            //}

            this.EditValue = this.dateEdit1.DateTime;
        }

        /// <summary>
        /// 禁止修改高度
        /// </summary>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.Height = 21;
            this.dateEdit1.Width = (this.Width - 75) >= 0 ? this.Width - 75 : 0;
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
                return this.dateEdit1.DateTime;
            }
            set
            {
                bool canChange = true;
                if (EditValueChanging != null)
                {
                    EditValueChanging(this, new CancelEventArgs(canChange));
                }

                this.dateEdit1.EditValueChanged -= new EventHandler(dateTime_EditValueChanged);

                if (canChange == false) return;

                if (value == null || !value.HasValue || value.Value.Year == 1)
                {
                    dateEdit1.EditValue = null;
                }
                else
                {
                    dateEdit1.DateTime = value.Value;
                }

                if (this.DataBindings["EditValue"] != null)
                {
                    this.DataBindings["EditValue"].WriteValue();
                }
                this.dateEdit1.EditValueChanged += new EventHandler(dateTime_EditValueChanged);

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
                return this.dateEdit1.BackColor;
            }
            set
            {
                this.dateEdit1.BackColor = value;
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

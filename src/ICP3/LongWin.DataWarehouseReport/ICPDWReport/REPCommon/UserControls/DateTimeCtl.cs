using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace LongWin.DataWarehouseReport.WinUI
{
    /// <summary>
    /// UserControl1 的摘要说明。    ///  
    /// </summary>
    public class DateTimeCtl : System.Windows.Forms.UserControl
    {
        #region Controls

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rabThisMonth;
        private System.Windows.Forms.RadioButton rabLastMonth;
        private System.Windows.Forms.DateTimePicker dtFrom;
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.Container components = null;

        #endregion

        private DateTime MinValue;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DateTimePicker dtTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rabUserdefined;
        private System.Windows.Forms.RadioButton rabMonth;
        private System.Windows.Forms.ComboBox cmbMonth;
        private System.Windows.Forms.ComboBox cmbYear;
        private DateTime MaxValue;
        private ComboBox cmbYearList;
        private Label labYear;
        private int[] YearList;

        #region 构造函数

        public DateTimeCtl()
        {
            MinValue = new DateTime(1753, 1, 1);
            MaxValue = new DateTime(9998, 12, 31);

            // 该调用是 Windows.Forms 窗体设计器所必需的。
            InitializeComponent();

            // TODO: 在 InitializeComponent 调用后添加任何初始化
            this.InitControls();
            this.cmbYearList.SelectedIndex = 0;

        }

        #endregion

        private void InitControls()
        {
            this.YearList = new int[5];
            for (int i = 0; i < this.YearList.Length; i++)
            {
                this.YearList[i] = DateTime.Now.Year - i;
            }
            this.cmbYear.DataSource = this.YearList;

            this.cmbYear.SelectedIndex = 0;
            this.cmbMonth.SelectedIndex = DateTime.Now.Month - 1;
        }

        #region OnLoad
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (this.DefaultDateTimeType == DefaultType.LastMonth)
            {
                this.rabLastMonth.Checked = true;
                this.dtFrom.Enabled = false;
                this.dtTo.Enabled = false;
                this.cmbYear.Enabled = false;
                this.cmbMonth.Enabled = false;
            }
            if (this.DefaultDateTimeType == DefaultType.ThisMonth)
            {

                this.rabThisMonth.Checked = true;
                this.dtFrom.Enabled = false;
                this.dtTo.Enabled = false;
                this.cmbYear.Enabled = false;
                this.cmbMonth.Enabled = false;
            }
            if (this.DefaultDateTimeType == DefaultType.Dentify)
            {
                this.rabUserdefined.Checked = true;
                this.dtFrom.Enabled = true;
                this.dtTo.Enabled = true;
                this.cmbYear.Enabled = false;
                this.cmbMonth.Enabled = false;
            }
            if (this.DefaultDateTimeType == DefaultType.DentifyMonth)
            {
                this.rabMonth.Checked = true;

                this.dtFrom.Enabled = false;
                this.dtTo.Enabled = false;
                this.cmbYear.Enabled = true;
                this.cmbMonth.Enabled = true;
            }
            if (this.DefaultFrom > this.MinValue
                && this.DefaultFrom < this.MaxValue
                && this.DefaultTo > this.MinValue
                && this.DefaultTo < this.MaxValue)
            {
                this.dtFrom.Value = this.DefaultFrom;
                this.dtTo.Value = this.DefaultTo;
            }
            else
            {
                this.dtFrom.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                this.dtTo.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
            }
            this.labYear.Visible = false;

        }
        #endregion

        #region Dispose
        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        #endregion

        #region 组件设计器生成的代码
        /// <summary> 
        /// 设计器支持所需的方法 - 不要使用代码编辑器 
        /// 修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DateTimeCtl));
            this.rabThisMonth = new System.Windows.Forms.RadioButton();
            this.rabLastMonth = new System.Windows.Forms.RadioButton();
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbMonth = new System.Windows.Forms.ComboBox();
            this.cmbYear = new System.Windows.Forms.ComboBox();
            this.rabMonth = new System.Windows.Forms.RadioButton();
            this.rabUserdefined = new System.Windows.Forms.RadioButton();
            this.labYear = new System.Windows.Forms.Label();
            this.cmbYearList = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // rabThisMonth
            // 
            resources.ApplyResources(this.rabThisMonth, "rabThisMonth");
            this.rabThisMonth.BackColor = System.Drawing.Color.Transparent;
            this.rabThisMonth.Name = "rabThisMonth";
            this.rabThisMonth.UseVisualStyleBackColor = false;
            this.rabThisMonth.CheckedChanged += new System.EventHandler(this.rabThisMonth_CheckedChanged);
            // 
            // rabLastMonth
            // 
            resources.ApplyResources(this.rabLastMonth, "rabLastMonth");
            this.rabLastMonth.BackColor = System.Drawing.Color.Transparent;
            this.rabLastMonth.Name = "rabLastMonth";
            this.rabLastMonth.UseVisualStyleBackColor = false;
            this.rabLastMonth.CheckedChanged += new System.EventHandler(this.rabLastMonth_CheckedChanged);
            // 
            // dtFrom
            // 
            this.dtFrom.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtFrom.CalendarTitleForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.dtFrom, "dtFrom");
            this.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.ValueChanged += new System.EventHandler(this.dtFrom_ValueChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.cmbMonth);
            this.panel1.Controls.Add(this.cmbYear);
            this.panel1.Controls.Add(this.rabMonth);
            this.panel1.Controls.Add(this.rabUserdefined);
            this.panel1.Controls.Add(this.rabLastMonth);
            this.panel1.Controls.Add(this.rabThisMonth);
            this.panel1.Controls.Add(this.labYear);
            this.panel1.Controls.Add(this.cmbYearList);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // cmbMonth
            // 
            resources.ApplyResources(this.cmbMonth, "cmbMonth");
            this.cmbMonth.BackColor = System.Drawing.Color.White;
            this.cmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonth.Items.AddRange(new object[] {
            resources.GetString("cmbMonth.Items"),
            resources.GetString("cmbMonth.Items1"),
            resources.GetString("cmbMonth.Items2"),
            resources.GetString("cmbMonth.Items3"),
            resources.GetString("cmbMonth.Items4"),
            resources.GetString("cmbMonth.Items5"),
            resources.GetString("cmbMonth.Items6"),
            resources.GetString("cmbMonth.Items7"),
            resources.GetString("cmbMonth.Items8"),
            resources.GetString("cmbMonth.Items9"),
            resources.GetString("cmbMonth.Items10"),
            resources.GetString("cmbMonth.Items11")});
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.SelectedIndexChanged += new System.EventHandler(this.cmbYear_SelectedIndexChanged);
            // 
            // cmbYear
            // 
            this.cmbYear.BackColor = System.Drawing.Color.White;
            this.cmbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cmbYear, "cmbYear");
            this.cmbYear.Name = "cmbYear";
            this.cmbYear.SelectedIndexChanged += new System.EventHandler(this.cmbYear_SelectedIndexChanged);
            // 
            // rabMonth
            // 
            resources.ApplyResources(this.rabMonth, "rabMonth");
            this.rabMonth.BackColor = System.Drawing.Color.Transparent;
            this.rabMonth.Name = "rabMonth";
            this.rabMonth.UseVisualStyleBackColor = false;
            this.rabMonth.CheckedChanged += new System.EventHandler(this.rabMonth_CheckedChanged);
            // 
            // rabUserdefined
            // 
            resources.ApplyResources(this.rabUserdefined, "rabUserdefined");
            this.rabUserdefined.BackColor = System.Drawing.Color.Transparent;
            this.rabUserdefined.Name = "rabUserdefined";
            this.rabUserdefined.UseVisualStyleBackColor = false;
            this.rabUserdefined.CheckedChanged += new System.EventHandler(this.rabUserdefined_CheckedChanged);
            // 
            // labYear
            // 
            resources.ApplyResources(this.labYear, "labYear");
            this.labYear.BackColor = System.Drawing.Color.Transparent;
            this.labYear.Name = "labYear";
            // 
            // cmbYearList
            // 
            resources.ApplyResources(this.cmbYearList, "cmbYearList");
            this.cmbYearList.BackColor = System.Drawing.Color.White;
            this.cmbYearList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYearList.Items.AddRange(new object[] {
            resources.GetString("cmbYearList.Items"),
            resources.GetString("cmbYearList.Items1"),
            resources.GetString("cmbYearList.Items2"),
            resources.GetString("cmbYearList.Items3")});
            this.cmbYearList.Name = "cmbYearList";
            this.cmbYearList.SelectedIndexChanged += new System.EventHandler(this.cmbYearList_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Window;
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.Window;
            this.panel4.Controls.Add(this.dtTo);
            this.panel4.Controls.Add(this.label2);
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Name = "panel4";
            // 
            // dtTo
            // 
            this.dtTo.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtTo.CalendarTitleForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.dtTo, "dtTo");
            this.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtTo.Name = "dtTo";
            this.dtTo.ValueChanged += new System.EventHandler(this.dtTo_ValueChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Name = "label2";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dtFrom);
            this.panel3.Controls.Add(this.label1);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // DateTimeCtl
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "DateTimeCtl";
            resources.ApplyResources(this, "$this");
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        #region 自定义属性

        private DateTime _DateTimeFrom;
        /// <summary>
        /// 返回的开始时间
        /// </summary>
        public DateTime DateTimeFrom
        {
            get
            {
                if (this._DateTimeFrom < this.MinValue || this._DateTimeFrom > this.MaxValue)
                {
                    this._DateTimeFrom = this.MinValue;
                }
                return this._DateTimeFrom;
            }
            set
            {
                this._DateTimeFrom = value;
            }
        }
        private DateTime _DateTimeTo;
        /// <summary>
        /// 返回的结束时间
        /// </summary>
        public DateTime DateTimeTo
        {
            get
            {
                if (this._DateTimeTo < this.MinValue || this._DateTimeTo > this.MaxValue)
                {
                    this._DateTimeTo = this.MaxValue;
                }
                return this._DateTimeTo;
            }
            set
            {
                this._DateTimeTo = value;
            }
        }

        public enum DefaultType
        {
            ThisMonth,
            LastMonth,
            DentifyMonth,
            Dentify
        }
        private DefaultType _DefaultDateTimeType;
        /// <summary>
        /// 默认时间段
        /// </summary>
        public DefaultType DefaultDateTimeType
        {
            get
            {
                return this._DefaultDateTimeType;
            }
            set
            {
                this._DefaultDateTimeType = value;
            }
        }


        private DateTime _DefaultFrom = DateTime.MinValue;
        /// <summary>
        /// 默认自定义的开始时间
        /// </summary>
        public DateTime DefaultFrom
        {
            get
            {
                return this._DefaultFrom;
            }
            set
            {
                this._DefaultFrom = value;
            }
        }
        private DateTime _DefaultTo = DateTime.MinValue;
        /// <summary>
        /// 默认的自定义结束时间
        /// </summary>
        public DateTime DefaultTo
        {
            get
            {
                return this._DefaultTo;
            }
            set
            {
                this._DefaultTo = value;
            }
        }

        #endregion

        #region 单选按钮切换事件
        /// <summary>
        /// 自定义时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rabUserdefined_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.rabUserdefined.Checked)
            {
                this.dtFrom.Enabled = true;
                this.dtTo.Enabled = true;
                this.DateTimeFrom = new DateTime(this.dtFrom.Value.Year, this.dtFrom.Value.Month, this.dtFrom.Value.Day, 0, 0, 0, 0);
                this.DateTimeTo = new DateTime(this.dtTo.Value.Year, this.dtTo.Value.Month, this.dtTo.Value.Day, 0, 0, 0, 0);
            }
            else
            {
                this.dtFrom.Enabled = false;
                this.dtTo.Enabled = false;

            }
        }

        /// <summary>
        /// 这个月
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rabThisMonth_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.rabThisMonth.Checked)
            {
                if (this.ConditionType == ConditionDateType.Year)
                {
                    this.DateTimeFrom = new DateTime(DateTime.Now.Year, 1, 1);
                    this.DateTimeTo = DateTime.Now;
                    this.dtFrom.Value = this.DateTimeFrom;
                    this.dtTo.Value = this.DateTimeTo;
                }
                else
                {
                    this.DateTimeFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    this.DateTimeTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
                    this.dtFrom.Value = this.DateTimeFrom;
                    this.dtTo.Value = this.DateTimeTo;
                }
            }

        }

        /// <summary>
        /// 上个月
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rabLastMonth_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.rabLastMonth.Checked)
            {
                if (this.ConditionType == ConditionDateType.Year)
                {
                    this.DateTimeFrom = new DateTime(DateTime.Now.Year - 1, 1, 1);
                    this.DateTimeTo = new DateTime(DateTime.Now.Year,1,1).AddDays(-1);
                    this.dtFrom.Value = this.DateTimeFrom;
                    this.dtTo.Value = this.DateTimeTo;
                }
                else
                {
                    this.DateTimeFrom = new DateTime(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month, 1);
                    this.DateTimeTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1);
                    this.dtFrom.Value = this.DateTimeFrom;
                    this.dtTo.Value = this.DateTimeTo;
                }
            }
        }

        /// <summary>
        /// 自定义选择月份
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rabMonth_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.rabMonth.Checked)
            {
                this.cmbYear.Enabled = true;
                this.cmbMonth.Enabled = true;
                this.cmbYearList.Enabled = true;

                int Year = this.YearList[this.cmbYear.SelectedIndex];
                int Month = this.cmbMonth.SelectedIndex + 1;

                this.DateTimeFrom = new DateTime(Year, Month, 1);
                //判断是不是选择的当前的月份
                if (Year == DateTime.Now.Year && Month == DateTime.Now.Month)
                {
                    this.DateTimeTo = DateTimeFrom.AddMonths(1).AddDays(-1);
                }
                else
                {
                    this.DateTimeTo = this.DateTimeFrom.AddMonths(1).AddDays(-1);
                }

                this.dtFrom.Value = this.DateTimeFrom;
                this.dtTo.Value = this.DateTimeTo;

                if (this.ConditionType == ConditionDateType.Year)
                {
                    this.cmbYearList.SelectedIndex = 0;
                    cmbYearList_SelectedIndexChanged(null, null);
                }

            }
            else
            {
                this.cmbYear.Enabled = false;
                this.cmbMonth.Enabled = false;
                this.cmbYearList.Enabled = false;
            }
        }
        #endregion

        #region 时间选择改变
        private void dtFrom_ValueChanged(object sender, System.EventArgs e)
        {
            if (this.rabUserdefined.Checked)
            {
                this.DateTimeFrom = new DateTime(this.dtFrom.Value.Year, this.dtFrom.Value.Month, this.dtFrom.Value.Day, 0, 0, 0, 0);
            }
        }

        private void dtTo_ValueChanged(object sender, System.EventArgs e)
        {
            if (this.rabUserdefined.Checked)
            {
                this.DateTimeTo = new DateTime(this.dtTo.Value.Year, this.dtTo.Value.Month, this.dtTo.Value.Day, 0, 0, 0, 0);
            }
        }
        #endregion

        private void cmbYear_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            int Year = this.YearList[this.cmbYear.SelectedIndex];
            int Month = this.cmbMonth.SelectedIndex + 1;
            if (Month == 0) Month++;
            this.DateTimeFrom = new DateTime(Year, Month, 1);
            //if (Year == DateTime.Now.Year && Month == DateTime.Now.Month)
            //{
            //    this.DateTimeTo = new DateTime(DateTime.Now.Year, this.dtTo.Value.Month, this.dtTo.Value.Day, 0, 0, 0, 0);
            //}
            //else
            //{
                this.DateTimeTo = this.DateTimeFrom.AddMonths(1).AddDays(-1);
            //}
            this.dtFrom.Value = this.DateTimeFrom;
            this.dtTo.Value = this.DateTimeTo;
        }

        ConditionDateType _conditionType = ConditionDateType.Month;

        public ConditionDateType ConditionType
        {
            get { return _conditionType; }
            set
            {
                
                _conditionType = value;
                if (_conditionType == ConditionDateType.Month)
                {
                    
                    rabLastMonth.Text = Utility.GetValueString("上个月", "上个月") ;
                    rabThisMonth.Text = Utility.GetValueString("本月", "本月") ;
                    this.cmbYearList.Visible = false;
                    this.cmbMonth.Visible = true;
                    this.cmbYear.Visible = true;
                    this.labYear.Visible = true;
                }
                else
                {
                    rabLastMonth.Text = Utility.GetValueString("去年", "去年");
                    rabThisMonth.Text = Utility.GetValueString("今年", "今年"); 
                    this.cmbYearList.Visible = true;
                    this.cmbMonth.Visible = false;
                    this.cmbYear.Visible = false;
                    this.labYear.Visible = false;
                    rabThisMonth_CheckedChanged(null, null);

                }
            }
        }
       


      

        private void cmbYearList_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*近三个月
            近六个月
            近三年
            所有*/

            DateTime beginTime= DateTime.Now;
            DateTime endTime= DateTime.Now;
            if (this.cmbYearList.SelectedIndex == 0)
            {
                beginTime = new DateTime(DateTime.Now.AddMonths(-2).Year, DateTime.Now.AddMonths(-2).Month , 1);
                endTime = DateTime.Now;
            }

            if (this.cmbYearList.SelectedIndex == 1)
            {
                beginTime = new DateTime(DateTime.Now.AddMonths(-5).Year, DateTime.Now.AddMonths(-5).Month , 1);
                endTime = DateTime.Now;
            }

            if (this.cmbYearList.SelectedIndex == 2)
            {
                beginTime = new DateTime(DateTime.Now.Year -2, 1, 1);
                endTime = DateTime.Now;
            }

            if (this.cmbYearList.SelectedIndex == 3)
            {
                beginTime = new DateTime(1900, 1, 1);
                endTime = DateTime.Now;
            }
            this.dtFrom.Value = beginTime;
            this.DateTimeFrom = beginTime;
            this.dtTo.Value = endTime;
            this.DateTimeTo = endTime;
        }

    }

    public enum ConditionDateType
    {
        /// <summary>
        /// 月
        /// </summary>
        Month,
        /// <summary>
        /// 年
        /// </summary>
        Year
    }
}

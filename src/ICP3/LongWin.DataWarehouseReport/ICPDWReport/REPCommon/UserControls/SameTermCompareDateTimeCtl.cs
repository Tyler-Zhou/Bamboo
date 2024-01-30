using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace LongWin.DataWarehouseReport.WinUI
{
    /// <summary>
    /// UserControl1 的摘要说明。
    /// </summary>
    public class SameTermCompareDateTimeCtl : System.Windows.Forms.UserControl
    {
        #region Controls

        private System.Windows.Forms.Label label1;
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
        private DateTime MaxValue;
        private ComboBox cmbYearList;
        private CheckBox checkBox12;
        private CheckBox checkBox11;
        private CheckBox checkBox10;
        private CheckBox checkBox9;
        private CheckBox checkBox8;
        private CheckBox checkBox7;
        private CheckBox checkBox6;
        private CheckBox checkBox5;
        private CheckBox checkBox4;
        private CheckBox checkBox3;
        private CheckBox checkBox2;
        private CheckBox checkBox1;
        private Label label3;
        //private int[] YearList;

        #region 构造函数

        public SameTermCompareDateTimeCtl()
        {
            MinValue = new DateTime(1753, 1, 1);
            MaxValue = new DateTime(9998, 12, 31);

            // 该调用是 Windows.Forms 窗体设计器所必需的。
            InitializeComponent();

            // TODO: 在 InitializeComponent 调用后添加任何初始化

        }

        #endregion


        #region OnLoad
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (this.DefaultDateTimeType == DefaultType.LastMonth)
            {
                this.dtFrom.Enabled = false;
                this.dtTo.Enabled = false;
            }
            if (this.DefaultDateTimeType == DefaultType.ThisMonth)
            {

                this.dtFrom.Enabled = false;
                this.dtTo.Enabled = false;
            }
            if (this.DefaultDateTimeType == DefaultType.Dentify)
            {
                this.rabUserdefined.Checked = true;
                this.dtFrom.Enabled = true;
                this.dtTo.Enabled = true;
            }
            if (this.DefaultDateTimeType == DefaultType.DentifyMonth)
            {
                this.rabMonth.Checked = true;

                this.dtFrom.Enabled = false;
                this.dtTo.Enabled = false;
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
                this.dtTo.Value = DateTime.Now;
            }
            this.cmbYearList.SelectedIndex = 0;

            this.checkBox1.Checked = false;
            this.checkBox2.Checked = false;
            this.checkBox3.Checked = false;
            this.checkBox4.Checked = false;
            this.checkBox5.Checked = false;
            this.checkBox6.Checked = false;
            this.checkBox7.Checked = false;
            this.checkBox8.Checked = false;
            this.checkBox9.Checked = false;
            this.checkBox10.Checked = false;
            this.checkBox11.Checked = false;
            this.checkBox12.Checked = false;

            rabMonth.Checked = true;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SameTermCompareDateTimeCtl));
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBox12 = new System.Windows.Forms.CheckBox();
            this.cmbYearList = new System.Windows.Forms.ComboBox();
            this.checkBox11 = new System.Windows.Forms.CheckBox();
            this.rabMonth = new System.Windows.Forms.RadioButton();
            this.checkBox10 = new System.Windows.Forms.CheckBox();
            this.checkBox9 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBox8 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rabUserdefined = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtFrom
            // 
            resources.ApplyResources(this.dtFrom, "dtFrom");
            this.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.ValueChanged += new System.EventHandler(this.dtFrom_ValueChanged);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.checkBox12);
            this.panel1.Controls.Add(this.cmbYearList);
            this.panel1.Controls.Add(this.checkBox11);
            this.panel1.Controls.Add(this.rabMonth);
            this.panel1.Controls.Add(this.checkBox10);
            this.panel1.Controls.Add(this.checkBox9);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.checkBox8);
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.checkBox7);
            this.panel1.Controls.Add(this.checkBox2);
            this.panel1.Controls.Add(this.checkBox6);
            this.panel1.Controls.Add(this.checkBox3);
            this.panel1.Controls.Add(this.checkBox5);
            this.panel1.Controls.Add(this.checkBox4);
            this.panel1.Controls.Add(this.panel2);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // checkBox12
            // 
            resources.ApplyResources(this.checkBox12, "checkBox12");
            this.checkBox12.Name = "checkBox12";
            this.checkBox12.UseVisualStyleBackColor = true;
            this.checkBox12.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // cmbYearList
            // 
            resources.ApplyResources(this.cmbYearList, "cmbYearList");
            this.cmbYearList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYearList.Items.AddRange(new object[] {
            resources.GetString("cmbYearList.Items"),
            resources.GetString("cmbYearList.Items1"),
            resources.GetString("cmbYearList.Items2"),
            resources.GetString("cmbYearList.Items3")});
            this.cmbYearList.Name = "cmbYearList";
            this.cmbYearList.SelectedIndexChanged += new System.EventHandler(this.cmbYearList_SelectedIndexChanged);
            // 
            // checkBox11
            // 
            resources.ApplyResources(this.checkBox11, "checkBox11");
            this.checkBox11.Name = "checkBox11";
            this.checkBox11.UseVisualStyleBackColor = true;
            this.checkBox11.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // rabMonth
            // 
            resources.ApplyResources(this.rabMonth, "rabMonth");
            this.rabMonth.Name = "rabMonth";
            this.rabMonth.CheckedChanged += new System.EventHandler(this.rabMonth_CheckedChanged);
            // 
            // checkBox10
            // 
            resources.ApplyResources(this.checkBox10, "checkBox10");
            this.checkBox10.Name = "checkBox10";
            this.checkBox10.UseVisualStyleBackColor = true;
            this.checkBox10.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // checkBox9
            // 
            resources.ApplyResources(this.checkBox9, "checkBox9");
            this.checkBox9.Name = "checkBox9";
            this.checkBox9.UseVisualStyleBackColor = true;
            this.checkBox9.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // checkBox8
            // 
            resources.ApplyResources(this.checkBox8, "checkBox8");
            this.checkBox8.Name = "checkBox8";
            this.checkBox8.UseVisualStyleBackColor = true;
            this.checkBox8.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // checkBox1
            // 
            resources.ApplyResources(this.checkBox1, "checkBox1");
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // checkBox7
            // 
            resources.ApplyResources(this.checkBox7, "checkBox7");
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.UseVisualStyleBackColor = true;
            this.checkBox7.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // checkBox2
            // 
            resources.ApplyResources(this.checkBox2, "checkBox2");
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // checkBox6
            // 
            resources.ApplyResources(this.checkBox6, "checkBox6");
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.UseVisualStyleBackColor = true;
            this.checkBox6.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // checkBox3
            // 
            resources.ApplyResources(this.checkBox3, "checkBox3");
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // checkBox5
            // 
            resources.ApplyResources(this.checkBox5, "checkBox5");
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.UseVisualStyleBackColor = true;
            this.checkBox5.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // checkBox4
            // 
            resources.ApplyResources(this.checkBox4, "checkBox4");
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
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
            resources.ApplyResources(this.dtTo, "dtTo");
            this.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtTo.Name = "dtTo";
            this.dtTo.ValueChanged += new System.EventHandler(this.dtTo_ValueChanged);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dtFrom);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.rabUserdefined);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // rabUserdefined
            // 
            resources.ApplyResources(this.rabUserdefined, "rabUserdefined");
            this.rabUserdefined.Name = "rabUserdefined";
            this.rabUserdefined.CheckedChanged += new System.EventHandler(this.rabUserdefined_CheckedChanged);
            // 
            // SameTermCompareDateTimeCtl
            // 
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.panel1);
            this.Name = "SameTermCompareDateTimeCtl";
            resources.ApplyResources(this, "$this");
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
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
        /// 自定义选择月份
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rabMonth_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.rabMonth.Checked)
            {
                this.cmbYearList.Enabled = true;                

            }
            else
            {
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

    


      

        private void cmbYearList_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*             
             近二年
             近三年
             近五年
            所有*/

            DateTime beginTime= DateTime.Now;
            DateTime endTime= DateTime.Now;
            if (this.cmbYearList.SelectedIndex == 0)
            {
                beginTime = new DateTime(DateTime.Now.AddYears(-1).Year, 1 , 1);
                endTime = DateTime.Now;
            }

            if (this.cmbYearList.SelectedIndex == 1)
            {
                beginTime = new DateTime(DateTime.Now.AddYears(-2).Year, 1, 1);
                endTime = DateTime.Now;
            }

            if (this.cmbYearList.SelectedIndex == 2)
            {
                beginTime = new DateTime(DateTime.Now.AddYears(-4).Year, 1, 1);
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

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            _monthStr = "0";
            if (checkBox1.Checked)
            {
                _monthStr = _monthStr + "," + "1";
            }
            if (checkBox2.Checked)
            {
                _monthStr = _monthStr + "," + "2";
            }
            if (checkBox3.Checked)
            {
                _monthStr = _monthStr + "," + "3";
            }
            if (checkBox4.Checked)
            {
                _monthStr = _monthStr + "," + "4";
            }
            if (checkBox5.Checked)
            {
                _monthStr = _monthStr + "," + "5";
            }
            if (checkBox6.Checked)
            {
                _monthStr = _monthStr + "," + "6";
            }
            if (checkBox7.Checked)
            {
                _monthStr = _monthStr + "," + "7";
            }
            if (checkBox8.Checked)
            {
                _monthStr = _monthStr + "," + "8";
            }
            if (checkBox9.Checked)
            {
                _monthStr = _monthStr + "," + "9";
            }
            if (checkBox10.Checked)
            {
                _monthStr = _monthStr + "," + "10";
            }
            if (checkBox11.Checked)
            {
                _monthStr = _monthStr + "," + "11";
            }
            if (checkBox12.Checked)
            {
                _monthStr = _monthStr + "," + "12";
            }
           
        }

        string _monthStr = string.Empty;
        public string MonthSelectedStr
        {
            get
            {               
                _monthStr = _monthStr.Replace("0,", "");
                _monthStr = _monthStr.Replace("111,", "10,11,");

                if (_monthStr == string.Empty 
                    || _monthStr == "0")
                {
                    _monthStr = "1,2,3,4,5,6,7,8,9,10,11,12";
                }
                return _monthStr;
            }
        }

    }

}

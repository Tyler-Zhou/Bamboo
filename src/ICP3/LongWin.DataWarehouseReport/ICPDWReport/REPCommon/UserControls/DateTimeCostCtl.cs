using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace LongWin.DataWarehouseReport.WinUI
{
	/// <summary>
	/// DateTimeCostCtl ��ժҪ˵����
	/// </summary>
	public class DateTimeCostCtl : System.Windows.Forms.UserControl
	{
		#region Controls

		private System.Windows.Forms.RadioButton rbLastMonth;
		private System.Windows.Forms.RadioButton rbThisMonth;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.RadioButton rbLastThreeMonth;
		private System.Windows.Forms.RadioButton rbLastSixMonth;
		private System.Windows.Forms.RadioButton rbSelf;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.Panel panel7;
		private System.Windows.Forms.Label label4;
		/// <summary> 
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

		#region var
		private DateTime MaxValue;
		private System.Windows.Forms.Panel panel8;
		private System.Windows.Forms.DateTimePicker dtFrom;
		private System.Windows.Forms.DateTimePicker dtTo;
		private DateTime MinValue;
		#endregion

		#region ���캯��

		public DateTimeCostCtl()
		{
			MinValue = new DateTime(1753,1,1);
			MaxValue = new DateTime(9998,12,31);
			// �õ����� Windows.Forms ���������������ġ�
			InitializeComponent();

			// TODO: �� InitializeComponent ���ú�����κγ�ʼ��
			
		}
		#endregion

		#region Onload
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (this.DefaultFrom > this.MinValue
                    && this.DefaultFrom < this.MaxValue
                    && this.DefaultTo > this.MinValue
                    && this.DefaultTo < this.MaxValue)
            {
                this.SetDate(this.DefaultFrom, this.DefaultTo);
            }
            else
            {
                DateTime From = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
                DateTime To = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);

                this.SetDate(From, To);
            }
            if (this.DefaultDateTimeType == DefaultType.LastMonth)
            {
                this.rbLastMonth.Checked = true;
                this.panel5.Enabled = false;
            }
            if (this.DefaultDateTimeType == DefaultType.ThisMonth)
            {

                this.rbThisMonth.Checked = true;
                this.panel5.Enabled = false;
            }
            if (this.DefaultDateTimeType == DefaultType.LastThreeMonth)
            {

                this.rbLastThreeMonth.Checked = true;
                this.panel5.Enabled = false;
            }
            if (this.DefaultDateTimeType == DefaultType.LastSixMonth)
            {
                this.rbLastSixMonth.Checked = true;
                this.panel5.Enabled = false;
            }
            if (this.DefaultDateTimeType == DefaultType.Dentify)
            {
                this.rbSelf.Checked = true;
                this.panel5.Enabled = true;
            }
        }
		
		#endregion

		#region ����ʱ��

		private void SetDate(DateTime From,DateTime To)
		{
			this.dtFrom.Value = From;
			this.dtTo.Value   = To;
		}
		#endregion

		#region Dispose

		/// <summary> 
		/// ������������ʹ�õ���Դ��
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#endregion

		#region �����������ɵĴ���
		/// <summary> 
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭�� 
		/// �޸Ĵ˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DateTimeCostCtl));
            this.rbThisMonth = new System.Windows.Forms.RadioButton();
            this.rbLastMonth = new System.Windows.Forms.RadioButton();
            this.rbLastSixMonth = new System.Windows.Forms.RadioButton();
            this.rbLastThreeMonth = new System.Windows.Forms.RadioButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.rbSelf = new System.Windows.Forms.RadioButton();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbThisMonth
            // 
            resources.ApplyResources(this.rbThisMonth, "rbThisMonth");
            this.rbThisMonth.BackColor = System.Drawing.Color.Transparent;
            this.rbThisMonth.Name = "rbThisMonth";
            this.rbThisMonth.UseVisualStyleBackColor = false;
            this.rbThisMonth.CheckedChanged += new System.EventHandler(this.rbThisMonth_CheckedChanged);
            // 
            // rbLastMonth
            // 
            resources.ApplyResources(this.rbLastMonth, "rbLastMonth");
            this.rbLastMonth.BackColor = System.Drawing.Color.Transparent;
            this.rbLastMonth.Name = "rbLastMonth";
            this.rbLastMonth.UseVisualStyleBackColor = false;
            this.rbLastMonth.CheckedChanged += new System.EventHandler(this.rbLastMonth_CheckedChanged);
            // 
            // rbLastSixMonth
            // 
            resources.ApplyResources(this.rbLastSixMonth, "rbLastSixMonth");
            this.rbLastSixMonth.BackColor = System.Drawing.Color.Transparent;
            this.rbLastSixMonth.Name = "rbLastSixMonth";
            this.rbLastSixMonth.UseVisualStyleBackColor = false;
            this.rbLastSixMonth.CheckedChanged += new System.EventHandler(this.rbLastSixMonth_CheckedChanged);
            // 
            // rbLastThreeMonth
            // 
            resources.ApplyResources(this.rbLastThreeMonth, "rbLastThreeMonth");
            this.rbLastThreeMonth.BackColor = System.Drawing.Color.Transparent;
            this.rbLastThreeMonth.Name = "rbLastThreeMonth";
            this.rbLastThreeMonth.UseVisualStyleBackColor = false;
            this.rbLastThreeMonth.CheckedChanged += new System.EventHandler(this.rbLastThreeMonth_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panel8);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel7);
            this.panel5.Controls.Add(this.panel6);
            resources.ApplyResources(this.panel5, "panel5");
            this.panel5.Name = "panel5";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.dtTo);
            this.panel7.Controls.Add(this.label4);
            resources.ApplyResources(this.panel7, "panel7");
            this.panel7.Name = "panel7";
            // 
            // dtTo
            // 
            this.dtTo.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtTo.CalendarTitleForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.dtTo, "dtTo");
            this.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTo.Name = "dtTo";
            this.dtTo.ValueChanged += new System.EventHandler(this.dtTo_ValueChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Name = "label4";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.dtFrom);
            this.panel6.Controls.Add(this.label1);
            resources.ApplyResources(this.panel6, "panel6");
            this.panel6.Name = "panel6";
            // 
            // dtFrom
            // 
            this.dtFrom.CalendarMonthBackground = System.Drawing.Color.White;
            this.dtFrom.CalendarTitleForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.dtFrom, "dtFrom");
            this.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.ValueChanged += new System.EventHandler(this.dtFrom_ValueChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.rbLastThreeMonth);
            this.panel8.Controls.Add(this.rbLastSixMonth);
            this.panel8.Controls.Add(this.rbLastMonth);
            this.panel8.Controls.Add(this.rbThisMonth);
            this.panel8.Controls.Add(this.rbSelf);
            resources.ApplyResources(this.panel8, "panel8");
            this.panel8.Name = "panel8";
            // 
            // rbSelf
            // 
            resources.ApplyResources(this.rbSelf, "rbSelf");
            this.rbSelf.BackColor = System.Drawing.Color.Transparent;
            this.rbSelf.Name = "rbSelf";
            this.rbSelf.UseVisualStyleBackColor = false;
            this.rbSelf.CheckedChanged += new System.EventHandler(this.rbSelf_CheckedChanged);
            // 
            // DateTimeCostCtl
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel3);
            this.Name = "DateTimeCostCtl";
            resources.ApplyResources(this, "$this");
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		#region �Զ�������

		#region �Զ���ʱ��ѡ���ѡ��Χ
		/// <summary>
		/// �Զ���ʱ��ѡ���ѡ��Χ
		/// </summary>
		public enum DefaultMonthType
		{
			//����º��ϸ��µ�Panel��ʾ
           LastandThisMonth,
			//ǰ�����º�������Panel��ʾ
			LastThreeandSixMonth,
			//�Զ���
			SelfMonth
		}
		#endregion

		#region Ĭ�ϼ���ʱ���ʱ��ѡ��Χ
		/// <summary>
		/// Ĭ�ϼ���ʱ���ʱ��ѡ��Χ
		/// </summary>
		public enum DefaultType
		{
			ThisMonth,
			LastMonth,
			LastThreeMonth,
			LastSixMonth,
			Dentify   
		}
		#endregion

		#region ���صĿ�ʼʱ��
		private DateTime _DateTimeFrom;
		/// <summary>
		/// ���صĿ�ʼʱ��
		/// </summary>
		public DateTime DateTimeFrom
		{
			get
			{
				if(this._DateTimeFrom <this.MinValue  ||this._DateTimeFrom >this.MaxValue)
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
		#endregion

		#region ���صĽ���ʱ��
		private DateTime _DateTimeTo;
		/// <summary>
		/// ���صĽ���ʱ��
		/// </summary>
		public DateTime DateTimeTo
		{
			get
			{
				if(this._DateTimeTo<this.MinValue ||this._DateTimeTo >this.MaxValue)
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
		#endregion

		#region Ĭ��ʱ���
		private DefaultType _DefaultDateTimeType = DefaultType.LastMonth;
		/// <summary>
		/// Ĭ��ʱ���
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
		#endregion

		#region Ĭ��ʱ��
		private DateTime _DefaultFrom = DateTime.MinValue;
		/// <summary>
		/// Ĭ���Զ���Ŀ�ʼʱ��
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
		#endregion

		#region Ĭ��ʱ��
		private DateTime _DefaultTo = DateTime.MinValue;
		/// <summary>
		/// Ĭ�ϵ��Զ������ʱ��
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

		#endregion

		#region ��ѡ��ť�л��¼�

		/// <summary>
		/// �ϸ���
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void rbLastMonth_CheckedChanged(object sender, System.EventArgs e)
		{
			if(this.rbLastMonth.Checked)
			{
				this.DateTimeFrom = new DateTime(DateTime.Now.Year,DateTime.Now.Month,1).AddMonths(-1);
				this.DateTimeTo   = new DateTime(DateTime.Now.Year,DateTime.Now.Month,1).AddDays(-1);
				
				this.SetDate(this.DateTimeFrom,this.DateTimeTo);
			}
		}

		/// <summary>
		/// �����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void rbThisMonth_CheckedChanged(object sender, System.EventArgs e)
		{
			if(this.rbThisMonth.Checked)
			{
				this.DateTimeFrom = new DateTime(DateTime.Now.Year,DateTime.Now.Month,1);
                this.DateTimeTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
                
				this.SetDate(this.DateTimeFrom,this.DateTimeTo);
			}
		}

		/// <summary>
		/// ǰ������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void rbLastThreeMonth_CheckedChanged(object sender, System.EventArgs e)
		{
			if(this.rbLastThreeMonth.Checked)
			{
				this.DateTimeFrom = new DateTime(DateTime.Now.Year,DateTime.Now.Month,1).AddMonths(-3);
				this.DateTimeTo   = new DateTime(DateTime.Now.Year,DateTime.Now.Month,1).AddDays(-1);
				
                this.SetDate(this.DateTimeFrom,this.DateTimeTo);
			}
		}

		/// <summary>
		/// ǰ������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void rbLastSixMonth_CheckedChanged(object sender, System.EventArgs e)
		{
			if(this.rbLastSixMonth.Checked)
			{
				this.DateTimeFrom = new DateTime(DateTime.Now.Year,DateTime.Now.Month,1).AddMonths(-6);
				this.DateTimeTo   = new DateTime(DateTime.Now.Year,DateTime.Now.Month,1).AddDays(-1);

				this.SetDate(this.DateTimeFrom,this.DateTimeTo);
			}
		}

		/// <summary>
		/// �Զ���
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void rbSelf_CheckedChanged(object sender, System.EventArgs e)
		{
			if(this.rbSelf.Checked)
			{
				this.panel5.Enabled = true;
				this.DateTimeFrom   = new DateTime(this.dtFrom.Value.Year,this.dtFrom.Value.Month,this.dtFrom.Value.Day,0,0,0,0);
				this.DateTimeTo     = new DateTime(this.dtTo.Value.Year,this.dtTo.Value.Month,this.dtTo.Value.Day,0,0,0,0);
			}
			else
			{ 
				this.panel5.Enabled= false;
			}
		}
		#endregion

		#region ʱ��ѡ��ı�
		private void dtFrom_ValueChanged(object sender, System.EventArgs e)
		{
			if(this.rbSelf.Checked)
			{
				this.DateTimeFrom = new DateTime(this.dtFrom.Value.Year,this.dtFrom.Value.Month,this.dtFrom.Value.Day,0,0,0,0);
			}
		}

		private void dtTo_ValueChanged(object sender, System.EventArgs e)
		{
			if(this.rbSelf.Checked)
			{
				this.DateTimeTo = new DateTime(this.dtTo.Value.Year,this.dtTo.Value.Month,this.dtTo.Value.Day,0,0,0,0);
			}
		}
		#endregion


	}
}

using System;
using ICP.Framework.ClientComponents.UIFramework;

namespace ICP.ReportCenter.UI.Comm.Controls
{
    public partial class OperationDateByMonthPart : BasePart
    {
        public OperationDateByMonthPart()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;

            //默认本月
            dteFrom.DateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); //本月头
            dteTo.DateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);//本月底
   
            this.cmbMonth.Properties.Items.Clear();
            this.cmbYear.Properties.Items.Clear();

            for (int i = 1; i < 13; i++)
            {
                cmbMonth.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(i.ToString(), (object)i));
            }
            for (int i = 0; i < 4; i++)
            {
                cmbYear.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem((DateTime.Now.Year - i).ToString(), (object)(DateTime.Now.Year - i)));
            }
           
            if (DateTime.Now.Month == 1)
            {
                //如果是当年第一月，则默认为上一年的12月
                cmbYear.EditValue = DateTime.Now.Year-1;
                cmbMonth.EditValue = 12;
            }
            else
            {
                //默认为本年的上一个月
                cmbYear.EditValue = DateTime.Now.Year;
                cmbMonth.EditValue = DateTime.Now.Month - 1;
            }
            cmbYear.Enabled = cmbMonth.Enabled = false;

            rdoSpecify.CheckedChanged += delegate
            {
                dteFrom.Enabled = dteTo.Enabled = rdoSpecify.Checked;
            };

            rdoCustom.CheckedChanged += delegate
            {
                cmbYear.Enabled = cmbMonth.Enabled = rdoCustom.Checked;
                if (rdoCustom.Checked) RefreshDateControl();
            };
            rdoLastMonth.CheckedChanged += delegate { if(rdoLastMonth.Checked)RefreshDateControl(); };
            rdoThisMonth.CheckedChanged += delegate { if (rdoThisMonth.Checked)RefreshDateControl(); };
            cmbMonth.SelectedIndexChanged += delegate { if (rdoCustom.Checked)RefreshDateControl(); };
            cmbYear.SelectedIndexChanged += delegate { if (rdoCustom.Checked)RefreshDateControl(); };
        }

        private void RefreshDateControl()
        {
            if (rdoSpecify.Checked) return;

            if (rdoCustom.Checked)
            {
                dteFrom.DateTime = new DateTime((int)cmbYear.EditValue, (int)cmbMonth.EditValue, 1);
                dteTo.DateTime =  new DateTime((int)cmbYear.EditValue, (int)cmbMonth.EditValue, 1).AddMonths(1).AddDays(-1);
            }
            else if (rdoLastMonth.Checked)
            {
                dteFrom.DateTime = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).AddMonths(-1);//上月头
                dteTo.DateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1);//上月底
            }
            else if (rdoThisMonth.Checked)
            {
                dteFrom.DateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); //本月头
                dteTo.DateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);//本月底
            }
        }

        public DateTime FromDate
        {
            get
            {
                return dteFrom.DateTime.Date;
            }
        }

        public DateTime ToDate
        {
            get
            {
                return Utility.GetEndDate(dteTo.DateTime);
            }
        }

        public void SetLastMonth()
        {
            this.rdoLastMonth.Checked = true;
        }
    }
}

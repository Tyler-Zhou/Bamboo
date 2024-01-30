using System;
using ICP.Framework.ClientComponents.UIFramework;

namespace ICP.ReportCenter.UI.Comm.Controls
{
    public partial class OperationDateCustomMonthPart : BasePart
    {
        public OperationDateCustomMonthPart()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;

            dteFrom.DateTime = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).AddMonths(-1);//上月头
            dteTo.DateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1);//上月底

            rdoSpecify.CheckedChanged += delegate
            {
                dteFrom.Enabled = dteTo.Enabled = rdoSpecify.Checked;
            };

            rdoThisMonth.CheckedChanged += delegate { if (rdoThisMonth.Checked)RefreshDateControl(); };
            rdoLastMonth.CheckedChanged += delegate { if (rdoLastMonth.Checked)RefreshDateControl(); };
            rdoThreeMonths.CheckedChanged += delegate { if (rdoThreeMonths.Checked)RefreshDateControl(); };
            rdoSixMonths.CheckedChanged += delegate { if (rdoSixMonths.Checked)RefreshDateControl(); };
        }

        private void RefreshDateControl()
        {
            if (rdoSpecify.Checked) return;

            if (rdoLastMonth.Checked)
            {
                dteFrom.DateTime = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).AddMonths(-1);//上月头
                dteTo.DateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1);//上月底
            }
            else if (rdoThisMonth.Checked)
            {
                dteFrom.DateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); //本月头
                dteTo.DateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);//本月底
            }
            if (rdoThreeMonths.Checked)
            {
                dteFrom.DateTime = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).AddMonths(-3);//3个月前 月头
                dteTo.DateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1);//上月底
            }
            else if (rdoSixMonths.Checked)
            {
                dteFrom.DateTime = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).AddMonths(-6);//6个月前 月头
                dteTo.DateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1);//上月底
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

    }
}

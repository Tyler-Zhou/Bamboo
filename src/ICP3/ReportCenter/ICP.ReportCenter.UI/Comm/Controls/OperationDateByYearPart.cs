using System;
using System.Collections.Generic;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.ReportCenter.UI.Comm.Controls
{
    public partial class OperationDateByYearPart : BasePart
    {
        public OperationDateByYearPart()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;

            dteFrom.DateTime = new DateTime(DateTime.Now.Year, 1, 1); //本年头
            dteTo.DateTime = new DateTime(DateTime.Now.Year, 1, 1).AddYears(1).AddDays(-1);//本年底

            List<EnumHelper.ListItem<DateMonthType>> types = EnumHelper.GetEnumValues<DateMonthType>(LocalData.IsEnglish);
            cmbMonthType.Properties.BeginUpdate();
            foreach (var item in types)
            {
                cmbMonthType.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbMonthType.Properties.EndUpdate();
            cmbMonthType.SelectedIndex = 0;

            rdoSpecify.CheckedChanged += delegate
            {
                dteFrom.Enabled = dteTo.Enabled = rdoSpecify.Checked;
            };

            rdoCustom.CheckedChanged += delegate
            {
                cmbMonthType.Enabled = rdoCustom.Checked;
                if (rdoCustom.Checked) RefreshDateControl();
            };
            rdoLastYear.CheckedChanged += delegate { if (rdoLastYear.Checked)RefreshDateControl(); };
            rdoThisYear.CheckedChanged += delegate { if (rdoThisYear.Checked)RefreshDateControl(); };
            cmbMonthType.SelectedIndexChanged += delegate { if (rdoCustom.Checked)RefreshDateControl(); };
            
        }

        private void RefreshDateControl()
        {
            if (rdoSpecify.Checked) return;

            if (rdoCustom.Checked)
            {
                DateMonthType dateMonthType = (DateMonthType)Enum.Parse(typeof(DateMonthType), cmbMonthType.EditValue.ToString());

                if (dateMonthType == DateMonthType.ThreeMonths)
                    dteFrom.DateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-2); //2个月前 -这个月月底
                else if (dateMonthType == DateMonthType.SixMonths)
                    dteFrom.DateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-5); //5个月前-这个月月底
                else if (dateMonthType == DateMonthType.ThreeYears)
                    dteFrom.DateTime = new DateTime(DateTime.Now.Year, 1, 1).AddYears(-2); //2年前-今年年底
                else if (dateMonthType == DateMonthType.All)
                    dteFrom.DateTime = new DateTime(1900, 1, 1);

                dteTo.DateTime = DateTime.Now;
            }
            else if (rdoLastYear.Checked)
            {
                dteFrom.DateTime = new DateTime(DateTime.Now.Year, 1, 1).AddYears(-1);//去年头
                dteTo.DateTime = new DateTime(DateTime.Now.Year, 1, 1).AddDays(-1);//去年底
            }
            else if (rdoThisYear.Checked)
            {
                dteFrom.DateTime = new DateTime(DateTime.Now.Year, 1, 1); //本年头
                dteTo.DateTime = DateTime.Now;
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

        private enum MonthType
        {
            近三个月,
            近六个月,
            近三年,
            所有

        }

        /// <summary>
        /// 日期类型
        /// </summary>
        public enum DateMonthType
        {
            [MemberDescription("近三个月", "Nearly three months")]
            ThreeMonths = 0,
            [MemberDescription("近六个月", "Nearly six months")]
            SixMonths = 1,
            [MemberDescription("近三年", "The last three years")]
            ThreeYears = 2,
            [MemberDescription("所有", "All")]
            All = 3
        }

    }
}

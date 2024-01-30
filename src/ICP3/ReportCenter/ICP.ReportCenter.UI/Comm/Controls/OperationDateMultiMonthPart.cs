using System;
using System.Collections.Generic;
using System.Text;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.ReportCenter.UI.Comm.Controls
{
    public partial class OperationDateMultiMonthPart : BasePart
    {
        public OperationDateMultiMonthPart()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (LocalData.IsDesignMode) return;

            List<EnumHelper.ListItem<DateYearType>> types = EnumHelper.GetEnumValues<DateYearType>(LocalData.IsEnglish);
            cmbYear.Properties.BeginUpdate();
            foreach (var item in types)
            {
                cmbYear.Properties.Items.Add(new DevExpress.XtraEditors.Controls.ImageComboBoxItem(item.Name, item.Value));
            }
            cmbYear.Properties.EndUpdate();
            cmbYear.SelectedIndex = 0;

        }

        public DateTime FromDate
        {
            get
            {
                DateYearType dateMonthType = (DateYearType)Enum.Parse(typeof(DateYearType), cmbYear.EditValue.ToString());

                if (dateMonthType == DateYearType.TwoYears)
                {
                    return new DateTime(DateTime.Now.Year, 1, 1).AddYears(-1); //1年前
                }
                else if (dateMonthType == DateYearType.ThreeYears)
                {
                    return new DateTime(DateTime.Now.Year, 1, 1).AddYears(-2); //3年前
                }
                else if (dateMonthType == DateYearType.FiveYears)
                {
                    return new DateTime(DateTime.Now.Year, 1, 1).AddYears(-4); //5年前
                }

                return new DateTime(1900, 1, 1);
            }
        }

        public DateTime ToDate
        {
            get
            {
                DateYearType dateMonthType = (DateYearType)Enum.Parse(typeof(DateYearType), cmbYear.EditValue.ToString());

                if (dateMonthType == DateYearType.All) return new DateTime(9998, 12, 31); 
                else return Utility.GetEndDate(DateTime.Now);
            }
        }

        /// <summary>
        /// CheckBox拼装的月份字符串
        /// </summary>
        public string MonthString
        {
            get
            {
                DateYearType dateMonthType = (DateYearType)Enum.Parse(typeof(DateYearType), cmbYear.EditValue.ToString());
                if (dateMonthType == DateYearType.All)
                {
                    return "1,2,3,4,5,6,7,8,9,10,11,12";
                }
                else
                {
                    StringBuilder builder = new StringBuilder();
                    if (chk1.Checked)
                    {
                        if (builder.Length > 0) builder.Append(","); builder.Append("1");
                    }
                    if (chk2.Checked)
                    {
                        if (builder.Length > 0) builder.Append(","); builder.Append("2");
                    }
                    if (chk3.Checked)
                    {
                        if (builder.Length > 0) builder.Append(","); builder.Append("3");
                    }
                    if (chk4.Checked)
                    {
                        if (builder.Length > 0) builder.Append(","); builder.Append("4");
                    }
                    if (chk5.Checked)
                    {
                        if (builder.Length > 0) builder.Append(","); builder.Append("5");
                    }
                    if (chk6.Checked)
                    {
                        if (builder.Length > 0) builder.Append(","); builder.Append("6");
                    }
                    if (chk7.Checked)
                    {
                        if (builder.Length > 0) builder.Append(","); builder.Append("7");
                    }
                    if (chk8.Checked)
                    {
                        if (builder.Length > 0) builder.Append(","); builder.Append("8");
                    }
                    if (chk9.Checked)
                    {
                        if (builder.Length > 0) builder.Append(","); builder.Append("9");
                    }
                    if (chk10.Checked)
                    {
                        if (builder.Length > 0) builder.Append(","); builder.Append("10");
                    }
                    if (chk11.Checked)
                    {
                        if (builder.Length > 0) builder.Append(","); builder.Append("11");
                    }
                    if (chk12.Checked)
                    {
                        if (builder.Length > 0) builder.Append(","); builder.Append("12");
                    }

                    if (builder.Length == 0) return "1,2,3,4,5,6,7,8,9,10,11,12";
                    else return builder.ToString();
                }
            }
        }


        /// <summary>
        /// 日期类型
        /// </summary>
        public enum DateYearType
        {
            [MemberDescription("近两年", "Nearly tow years")]
            TwoYears = 0,
            [MemberDescription("近三年", "Nearly three years")]
            ThreeYears = 1,
            [MemberDescription("近五年", "The last five years")]
            FiveYears = 2,
            [MemberDescription("所有", "All")]
            All = 3
        }
    }
}

using System;

namespace ICP.FCM.Common.UI
{
    public partial class FromToDateMonthControl : DevExpress.XtraEditors.XtraUserControl
    {
        #region Init

        public FromToDateMonthControl()
        {
            InitializeComponent();
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroup1.SelectedIndex == (int)DateType.Specify)
            {
                dteFrom.Enabled = dteTo.Enabled = true;
                dteFrom.DateTime = DateTime.Now.AddDays(-7);
                dteTo.DateTime = DateTime.Now.Date;
            }
            else
                dteFrom.Enabled = dteTo.Enabled = false;
        }

        enum DateType
        {
            Unknown,
            LastMonth,
            ThisMonth,
            Specify
        }

        #endregion

        #region Public

        public DateTime? From
        {
            get
            {
                if (radioGroup1.SelectedIndex == (int)DateType.Unknown)
                {
                    return null;
                }
                else if (radioGroup1.SelectedIndex == (int)DateType.LastMonth)
                {
                    DateTime dt = DateTime.Now.AddMonths(-1);
                    return new DateTime(dt.Year, dt.Month, 1);
                }
                else if (radioGroup1.SelectedIndex == (int)DateType.ThisMonth)
                {
                    return new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                }
                else
                {
                    
                    return dteFrom.DateTime.Date;
                }
            }
        }

        public DateTime? To
        {
            get
            {
                if (radioGroup1.SelectedIndex == (int)DateType.Unknown)
                {
                    return null;
                }
                else if (radioGroup1.SelectedIndex == (int)DateType.LastMonth)
                {
                    DateTime dt = Utility.GetEndDate(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1));//本月月头
                    return dt.AddDays(-1);
                }
                else if (radioGroup1.SelectedIndex == (int)DateType.ThisMonth)
                {
                    DateTime dt = DateTime.Now.AddMonths(1);//下月月头
                    dt = Utility.GetEndDate(new DateTime(dt.Year, dt.Month, 1));
                    return dt.AddDays(-1);
                }
                else
                {
                    return Utility.GetEndDate(dteTo.DateTime);
                }
            }
        }

        public string ToText
        {
            get { return labTo.Text; }
            set { labTo.Text = value; }
        }
        public string FromText
        {
            get { return labFrom.Text; }
            set { labFrom.Text = value; }
        }

        public string UnknownText
        {
            get { return radioGroup1.Properties.Items[0].Description; }
            set { radioGroup1.Properties.Items[0].Description = value; }
        }
        public string LastMonthText
        {
            get { return radioGroup1.Properties.Items[1].Description; }
            set { radioGroup1.Properties.Items[1].Description = value; }
        }
        public string ThisMonthText
        {
            get { return radioGroup1.Properties.Items[2].Description; }
            set { radioGroup1.Properties.Items[2].Description = value; }
        }
        public string SpecifyText
        {
            get { return radioGroup1.Properties.Items[3].Description; }
            set { radioGroup1.Properties.Items[3].Description = value; }
        }

        public int Distance
        {
            get { return panel1.Width; }
            set{panel1.Width = value;}
        }

        #endregion
    }
}

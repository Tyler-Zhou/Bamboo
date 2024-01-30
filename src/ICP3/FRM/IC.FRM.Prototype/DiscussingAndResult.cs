using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace IC.FRM.Prototype
{
    public partial class DiscussingAndResult : UserControl
    {
        public DiscussingAndResult()
        {
            InitializeComponent();

            LoadData();

        }

        int _Mode = 1;
        public int Mode
        {
            get
            {
                if (_Mode == 1)
                {
                    xtraTabControl1.TabPages[2].Hide();
                    xtraTabControl1.TabPages[1].Show();
                }
                else
                {
                    xtraTabControl1.TabPages[1].Hide();
                    xtraTabControl1.TabPages[2].Show();
                }
                return _Mode;
            }
            set
            {
                _Mode = value;

                if (_Mode == 1)
                {
                    xtraTabControl1.TabPages[2].Hide();
                    xtraTabControl1.TabPages[1].Show();
                }
                else
                {
                    xtraTabControl1.TabPages[1].Hide();
                    xtraTabControl1.TabPages[2].Show();
                }
            }
        }

        void LoadData()
        {
            List<InquireDiscussingData> inquireDiscussingData = new List<InquireDiscussingData>();
            inquireDiscussingData.Add(new InquireDiscussingData() 
            {
                From = "Kiho Li said at 19:00 8/18/2011",
                Contect = @"有多少柜呢？这个价格跟COSCO还是很接近的呀"
            });

            inquireDiscussingData.Add(new InquireDiscussingData()
            {
                From = "Eric Huang said at 13:00 8/18/2011",
                Contect = @"代理说想要USD750的价格，LANSHI 出。
Change Logs:
ETD is changed to 4/18/2011
Marks 'Need Fumigate'
"
            });

            inquireDiscussingData.Add(new InquireDiscussingData()
            {
                From = "Kiho Li said at 10:00 8/18/2011",
                Contect = @"那客人的目标价难道是跟RCL同价格段？"
            });

            inquireDiscussingData.Add(new InquireDiscussingData()
            {
                From = "Eric Huang said at 8:30 8/18/2011",
                Contect = @"现在主要是有些客人觉得COSCO的价格高了。
想要便宜的船东，然后代理让我去问问有没有其他的船东LANSHI 出的价格比较低"
            });


            gridControl1.DataSource = inquireDiscussingData;

        }

        private void labelControl4_Click(object sender, EventArgs e)
        {

        }

        private void textEdit6_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void textEdit2_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            (new Transit()).ShowDialog();
        }
    }
}

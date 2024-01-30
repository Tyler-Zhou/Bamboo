using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace IC.FRM.Prototype
{
    public partial class InquireList : UserControl
    {
        public InquireList()
        {
            InitializeComponent();

            LoadData();
            discussingAndResult1.Mode = 1;
        }

        void LoadData()
        {
            List<InquireOceanListData> inquireOceanListData = new List<InquireOceanListData>();
            inquireOceanListData.Add(new InquireOceanListData() 
                { 
                    Carrier = "CSCL",
                    _20GP = "8000",
                    _40GP = "9000",
                    _40HQ = "11000",
                    Commodity = "FAK",
                    Currency = "USD",
                    Duration_From = "9/18/2011",
                    Duration_To = "12/18/2011",
                    SurCharge = "",
                    Term = "CY-CY",
                    POL = "Yantian",
                    POD = "Los Angeles, CA",
                    Delivery = "Los Angeles, CA",
                });

            inquireOceanListData.Add(new InquireOceanListData()
            {
                Carrier = "CSCL",
                _20GP = "6000",
                _40GP = "7000",
                _40HQ = "8000",
                Commodity = "FAK",
                Currency = "USD",
                Duration_From = "9/18/2011",
                Duration_To = "12/18/2011",
                SurCharge = "",
                Term = "CY-CY",
                POL = "Yantian",
                POD = "HOUSTON,TX",
                Delivery = "HOUSTON,TX",
            });

            inquireOceanListData.Add(new InquireOceanListData()
            {
                Carrier = "CSCL",
                _20GP = "8000",
                _40GP = "9000",
                _40HQ = "11000",
                Commodity = "FAK",
                Currency = "USD",
                Duration_From = "9/18/2011",
                Duration_To = "12/18/2011",
                SurCharge = "",
                Term = "CY-CY",
                POL = "Yantian",
                POD = "BANGKOK",
                Delivery = "BANGKOK",
            });

            inquireOceanListData.Add(new InquireOceanListData()
            {
                Carrier = "COSCO",
                _20GP = "8500",
                _40GP = "9500",
                _40HQ = "12000",
                Commodity = "FAK",
                Currency = "USD",
                Duration_From = "9/18/2011",
                Duration_To = "12/18/2011",
                SurCharge = "",
                Term = "CY-CY",
                POL = "Yantian",
                POD = "BANGKOK",
                Delivery = "BANGKOK",
            });

            inquireOceanListData.Add(new InquireOceanListData()
            {
                Carrier = "MSC",
                _20GP = "7000",
                _40GP = "8000",
                _40HQ = "9500",
                Commodity = "FAK",
                Currency = "USD",
                Duration_From = "9/18/2011",
                Duration_To = "12/18/2011",
                SurCharge = "",
                Term = "CY-CY",
                POL = "Yantian",
                POD = "BANGKOK",
                Delivery = "BANGKOK",
            });

            gridControl1.DataSource = inquireOceanListData;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            (new InquireOpen2()).ShowDialog();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void dropDownButton1_Click(object sender, EventArgs e)
        {
            (new InquireOcean()).ShowDialog();
        }
    }
}

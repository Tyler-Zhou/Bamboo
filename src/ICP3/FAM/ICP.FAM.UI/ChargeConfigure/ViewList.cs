using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ICP.FAM.UI.ChargeConfigure
{
    public partial class ViewList : Form
    {
        public ViewList()
        {
            InitializeComponent();
        }

        private List<string> DataList;

        public void SetData(string data)
        {
            DataList = data.Split(',').ToList();
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            
        }

        private void ViewList_Load(object sender, EventArgs e)
        {
            foreach (string data in DataList)
            {
                listBox.Items.Add(data);
            }
        }
    }
}

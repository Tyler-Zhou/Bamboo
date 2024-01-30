using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using LongWin.ReportCenter.Model;

namespace LongWin.ReportCenter.Testing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int Count = 5;

            DateTime happenDate = DateTime.Now;
            Guid guid = Guid.NewGuid();
            decimal amount = 55;
            string no = "123456";
            short feeProperty = 0;


            REPCostFee costFee = new REPCostFee();
            costFee.HappenDate = happenDate;
            costFee.HappenPeriod = System.Convert.ToInt32(happenDate.Year.ToString().Trim() + happenDate.Month.ToString().Trim());
            costFee.DeptID = guid;
            costFee.UserID = guid;
            costFee.CompanyID = guid;
            costFee.Amount = amount;
            costFee.no = no;
            costFee.FeeProperty = feeProperty;

            Guid[] costItemIDs = new Guid[Count];
            short[] feePropertys = new short[Count];
            Guid[] firstCostItemIDs = new Guid[Count];
            decimal[] amounts = new decimal[Count];
            string[] remarks = new string[Count];

            for (int i = 0; i < Count; i++)
            {
                costItemIDs[i] = Guid.NewGuid();
                feePropertys[i] = 1;
                firstCostItemIDs[i] = Guid.NewGuid();
                amounts[i] = amount / Count;
                remarks[i] = amounts[i].ToString() + "/" + costItemIDs[i].ToString(); ;

            }

            costFee.SaveWithFee(costItemIDs, feePropertys, firstCostItemIDs, amounts, remarks);

            MessageBox.Show( costFee.ID.ToString());
        }
    }
}
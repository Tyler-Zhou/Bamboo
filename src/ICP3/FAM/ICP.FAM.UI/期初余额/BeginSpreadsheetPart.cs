using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary;

namespace ICP.FAM.UI
{
    public partial class BeginSpreadsheetPart : UserControl
    {
        public BeginSpreadsheetPart()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
            {
                ShowData();
            }
        }

        public List<BeginBalances> DataList
        {
            get;
            set;
        }
        private void ShowData()
        {
            decimal totalASSETS = 0, totalCOST = 0, totalLIABILITIES = 0, totalEQUITY = 0, totalINCOME = 0,totaDR=0,totalCR=0;
           
            #region 计算
            foreach (BeginBalances item in DataList)
            {
                if (item.GLCodeType == GLCodeType.ASSETS)
                { 
                    //资产
                    totalASSETS = totalASSETS +DataTypeHelper.GetDecimal(item.DRAmt,0) -DataTypeHelper.GetDecimal(item.CRAmt,0);
                }
                else if (item.GLCodeType == GLCodeType.COST)
                {
                    //成本
                    totalCOST = totalCOST + DataTypeHelper.GetDecimal(item.DRAmt, 0) - DataTypeHelper.GetDecimal(item.CRAmt, 0);
                }
                else if (item.GLCodeType == GLCodeType.LIABILITIES)
                {
                    //负债
                    totalLIABILITIES = totalLIABILITIES + DataTypeHelper.GetDecimal(item.CRAmt, 0) - DataTypeHelper.GetDecimal(item.DRAmt, 0);
                }
                else if (item.GLCodeType == GLCodeType.EQUITY)
                {
                    //损益
                    totalEQUITY = totalEQUITY + DataTypeHelper.GetDecimal(item.CRAmt, 0) - DataTypeHelper.GetDecimal(item.DRAmt, 0);
                }
                else if (item.GLCodeType == GLCodeType.INCOME)
                {
                    //权益
                    totalINCOME = totalINCOME + DataTypeHelper.GetDecimal(item.CRAmt, 0) - DataTypeHelper.GetDecimal(item.DRAmt, 0);
                }
            }
            #endregion

            #region 资产
            if (totalASSETS > 0)
            {
                labASSETS.Text = "资产 = 借 "+totalASSETS.ToString("N");
            }
            else if (totalASSETS < 0)
            {
                labASSETS.Text = "资产 = 贷 " +Math.Abs(totalASSETS).ToString("N");
            }
            else 
            {
                labASSETS.Text = "资产 = 平";
            }
            #endregion

            #region 成本
            if (totalCOST >0)
            {
                labCOST.Text = "成本 = 借 " + totalCOST.ToString("N");
            }
            else if (totalCOST < 0)
            {
                labCOST.Text = "成本 = 贷 " +Math.Abs(totalCOST).ToString("N");
            }
            else
            {
                labCOST.Text = "成本 = 平";
            }
            #endregion

            #region 负债
            if (totalLIABILITIES > 0)
            {
                labLIABILITIES.Text = "负债 = 贷 " + totalLIABILITIES.ToString("N");
            }
            else if (totalLIABILITIES < 0)
            {
                labLIABILITIES.Text = "负债 = 借 " +Math.Abs(totalLIABILITIES).ToString("N");
            }
            else
            {
                labLIABILITIES.Text = "负债 = 平";
            }
            #endregion

            #region 权益
            if (totalINCOME > 0)
            {
                labINCOME.Text = "权益 = 贷 " + totalINCOME.ToString("N");
            }
            else if (totalINCOME < 0)
            {
                labINCOME.Text = "权益 = 借 " +Math.Abs(totalINCOME).ToString("N");
            }
            else
            {
                labINCOME.Text = "权益 = 平";
            }
            #endregion

            #region 损益
            if (totalEQUITY > 0)
            {
                labEQUITY.Text = "损益 = 贷 " + totalEQUITY.ToString("N");
            }
            else if (totalEQUITY < 0)
            {
                labEQUITY.Text = "损益 = 借 " +Math.Abs(totalEQUITY).ToString("N");
            }
            else
            {
                labEQUITY.Text = "损益 = 平";
            }
            #endregion

            totaDR = totalASSETS + totalCOST;
            totalCR = totalLIABILITIES + totalEQUITY + totalINCOME;

            if (totaDR > 0)
            {
                labTotalDR.Text = "合计 借  " + (totaDR).ToString("N");
            }
            else if (totaDR < 0)
            {
                labTotalDR.Text = "合计 贷  " +Math.Abs(totaDR).ToString("N");
            }
            else
            {
                labTotalDR.Text = "平";
            }

            if (totalCR > 0)
            {
                labTotalCR.Text = "合计 贷  " + (totalCR).ToString("N");
            }
            else if (totalCR < 0)
            {
                labTotalCR.Text = "合计 借  " + Math.Abs(totalCR).ToString("N");
            }
            else
            {
                labTotalCR.Text = "平" ;
            }

            if (totaDR == totalCR)
            {
                labResult.Text = "试算结果平衡";
                labResult.ForeColor = Color.Blue;
            }
            else
            {
                labResult.Text = "试算结果不平衡";
                labResult.ForeColor = Color.Red;
            }
        }



        private void barClose_Click(object sender, EventArgs e)
        {
            if (FindForm() != null)
            {
                FindForm().Close();
            }
        }
    }
}

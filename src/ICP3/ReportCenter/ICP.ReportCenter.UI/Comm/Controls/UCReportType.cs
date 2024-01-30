using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ICP.FAM.ServiceInterface.DataObjects;
namespace ICP.ReportCenter.UI.Comm.Controls
{  
    /// <summary>
    /// 报表类型用户自定义控件
    /// </summary>
    public partial class UCReportType : XtraUserControl
    {
        public UCReportType()
        {
            InitializeComponent();
        }
        /// <summary>
        /// BillTypes
        /// </summary>
        public List<ReportBillType> BillTypes
        {
            get
            {
                List<ReportBillType> types = new List<ReportBillType>();

                if (chkAR.Checked)
                {
                    types.Add(ReportBillType.AR);
                }
                if (chkAP.Checked)
                {
                    types.Add(ReportBillType.AP);
                }
                if (chkDRCR.Checked)
                {
                    types.Add(ReportBillType.DRCR);
                }
                if (chkCheck.Checked)
                {
                    types.Add(ReportBillType.Check);
                }
                if (chkDeposit.Checked)
                {
                    types.Add(ReportBillType.Deposit);
                }
                if (chkJournal.Checked)
                {
                    types.Add(ReportBillType.Journal);
                }
                if (chkClearance.Checked)
                {
                    types.Add(ReportBillType.Clearance);
                }


                return types;
            }
        }
        /// <summary>
        /// RepotTypeString
        /// </summary>
        /// <returns></returns>
        public string RepotTypeString
        {
            get
            {
                string reportype = string.Empty;
                #region 获取报表类型

                if (chkAR.Checked)
                {
                    reportype = "AR,";
                }
                if (chkDeposit.Checked)
                {
                    reportype += "Deposit,";
                }
                if (chkDRCR.Checked)
                {
                    reportype += "CRDR,";
                }
                if (chkCheck.Checked)
                {
                    reportype += "Checked,";
                }
                if (chkAP.Checked)
                {
                    reportype += "AP,";
                }
                if (chkJournal.Checked)
                {
                    reportype += "Journal,";
                }
                if (chkClearance.Checked)
                {
                    reportype += "Clearance,";
                }

                if (reportype.Length > 0)
                {
                    reportype = reportype.Substring(0, reportype.Length - 1);
                }
                return reportype;
                #endregion
            }
        }
    }
}

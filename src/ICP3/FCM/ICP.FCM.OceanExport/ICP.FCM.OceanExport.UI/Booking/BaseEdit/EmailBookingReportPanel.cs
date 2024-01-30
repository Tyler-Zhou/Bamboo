using System;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Client;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects.ReportObjects;

namespace ICP.FCM.OceanExport.UI.Booking.BaseEdit
{
    /// <summary>
    /// 
    /// </summary>
    public partial class EmailBookingReportPanel : Form
    {
        /// <summary>
        /// 订舱ID
        /// </summary>
        public Guid BookingID { get; set; }
        /// <summary>
        /// 船东ID
        /// </summary>
        public Guid? CarrierID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public EmailBookingReportPanel()
        {
            InitializeComponent();
            //隐藏Panel
            pCSCL.Visible = false;
        }

        public OceanExportPrintHelper OceanExportPrintHelper
        {
            get
            {
                return ClientHelper.Get<OceanExportPrintHelper, OceanExportPrintHelper>();
            }
        }
        /// <summary>
        /// 默认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdoDefault_CheckedChanged(object sender, EventArgs e)
        {
            pCSCL.Visible = false;
        }
        /// <summary>
        /// 中海
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdoCSCL_CheckedChanged(object sender, EventArgs e)
        {
            pCSCL.Visible = true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            OtherEmailBookingField other = new OtherEmailBookingField();
            if (rdoCSCL.Checked)
            {
                other.PaymentSettledAtCN = rdoPayChina.Checked;
            }
            OceanExportPrintHelper.EmailBooking(BookingID, CarrierID, other);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}

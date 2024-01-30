#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/4/27 13:53:42
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using System.Windows.Forms;

namespace Cityocean.TaskSchedule.Client
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormSailingSchedule : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public FormSailingSchedule()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormSailingSchedule_Load(object sender, EventArgs e)
        {
            InitData();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddConfig_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnbtnSearchCSP_Click(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnbtnSearchInttra_Click(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCrawlData_Click(object sender, EventArgs e)
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        void InitData()
        {
            try
            {
                txtSOriginCode.Text = "119541";
                txtSOriginDesc.Text = "Halifax, Nova Scotia, Canada (CAHAL)";

                txtSDestinationCode.Text = "219161";
                txtSDestinationDesc.Text = "Chicago, Illinois, United States (USCHI)";

                txtSStartDate.Text = "2017-05-27";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

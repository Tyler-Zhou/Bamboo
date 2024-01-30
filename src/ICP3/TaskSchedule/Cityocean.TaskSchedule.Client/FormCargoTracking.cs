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

using Cityocean.Crawl.CommonLibrary;
using System;
using System.Windows.Forms;

namespace Cityocean.TaskSchedule.Client
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormCargoTracking : Form
    {
        /// <summary>
        /// Module Name
        /// </summary>
        public string ModuleName {
            get { return "CargoTracking"; } 
        }
        /// <summary>
        /// 
        /// </summary>
        public TimeSpan TSTimeOut
        {
            get { return new TimeSpan(0, 1, 0); }
        }

        /// <summary>
        /// 页面未完成等待时间:5s
        /// </summary>
        public TimeSpan TSUnCompelete
        {
            get { return new TimeSpan(0, 0, 5); }
        }

        /// <summary>
        /// 
        /// </summary>
        public int WebTimeOutMilliseconds
        {
            get { return Convert.ToInt32((new TimeSpan(0, 1, 0)).TotalMilliseconds); }
        }

        #region Config
        /// <summary>
        /// 获取提单信息URL
        /// </summary>
        public string URL_GetBillGroup
        {
            get
            {
                return GetConfigValue("SearchURL1");
            }
        }
        /// <summary>
        /// 获取箱明细
        /// </summary>
        public string URL_GetCtnrStatus
        {
            get
            {
                return GetConfigValue("SearchURL2");
            }
        }
        /// <summary>
        /// 卸船超时日期
        /// </summary>
        public int DischargeTimeOut
        {
            get
            {
                return 30;
            }
        }
        /// <summary>
        /// 等待时间
        /// </summary>
        public TimeSpan WaitTimeSpan
        {
            get { return new TimeSpan(0, 0, 5); }
        }
        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="paramKey">键值</param>
        /// <param name="paramDefaultValue">默认值</param>
        /// <returns></returns>
        public string GetConfigValue(string paramKey, string paramDefaultValue = "")
        {
            string strTemp = INIHelper.Instance.IniReadValue(ModuleName, paramKey);
            return strTemp.IsNullOrEmpty() ? paramDefaultValue : strTemp;
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        public FormCargoTracking()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormCargoTracking_Load(object sender, EventArgs e)
        {
            InitData();
            FillCarrierControl();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddCargoTrackingConfig_Click(object sender, EventArgs e)
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
        private void btnCSP_Click(object sender, EventArgs e)
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
        private void btnYunDangCarriers_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ea"></param>
        private void btnYunDang_Click(object sender, EventArgs ea)
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
        /// <param name="ea"></param>
        private void btnOfficialWebsite_Click(object sender, EventArgs ea)
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
        private void btnClawlerData_Click(object sender, EventArgs e)
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
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        void FillCarrierControl()
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

        private void btnDeserialize_Click(object sender, EventArgs e)
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
        private void btnSingleCrawlData_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}

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
using System.Collections.Generic;
using System.Windows.Forms;
using Cityocean.Crawl.ServiceInterface;

namespace Cityocean.TaskSchedule.Client
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FormTerminal : Form
    {
        #region 网站配置
        private List<CrawlConfig> _websiteConfigs;
        /// <summary>
        /// 网站配置
        /// </summary>
        public List<CrawlConfig> _WebsiteConfigs
        {
            get
            {
                return _websiteConfigs ??
                       (_websiteConfigs =
                           _websiteConfigs = new List<CrawlConfig>());
            }
            set
            {
                if (value == null && _websiteConfigs != null)
                    _websiteConfigs.Clear(); _websiteConfigs = value;
            }
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        public FormTerminal()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormTerminal_Load(object sender, EventArgs e)
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
        private void btnAddTerminalConfig_Click(object sender, EventArgs e)
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
        private void btnTerminal_Click(object sender, EventArgs e)
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
        /// <param name="e"></param>
        private void btnCrawlDataByContainerID_Click(object sender, EventArgs e)
        {
            try
            {
                //string containerNo = txtSContainerID.Text;
                //txtRError.Text = "";
                //txtRPageSource.Text = "";
                //TimeSpan TSTimeOut = new TimeSpan(0, 0, 60);
                //TaskCargoTracking taskItem = new TaskCargoTracking {ContainerID = containerNo.NewGuid()};
                //CrawlConfig websiteConfig = _WebsiteConfigs.SingleOrDefault(
                //    fItem => fItem.CrawlType == CrawlType.SailingSchedule && "94C1D35F-B9E5-43E1-9428-F2CFCB350727".Equals((fItem.ID + "").ToUpper()));
                //if (websiteConfig == null) return;
                //var crawlService = new WCrawler()
                //{
                //    StartAction = (paramStart) =>
                //    {
                //        CrawlConfig taskConfig = paramStart.TaskConfig as CrawlConfig;
                //        if (taskConfig == null)
                //        {
                //            throw new WebDriverException("CrawlStartAction WebsiteConfig Is Null");
                //        }
                //        paramStart.WebDriver.ExecActionByWebConfig((object)paramStart.TaskObject, taskConfig);
                //    },
                //    ErrorAction = (paramError) =>
                //    {
                //        TaskCargoTracking taskObject = paramError.TaskObject as TaskCargoTracking;
                //        if (taskObject == null) return;
                //        taskObject.HTMLDescription = paramError.Exception.Message;
                //        txtRError.Text = paramError.Exception.Message;
                //        txtRPageSource.Text = paramError.PageSource.GetBody();
                //    },
                //    CompletedAction = (eCA) =>
                //    {
                //        CrawlConfig taskConfig = eCA.TaskConfig as CrawlConfig;
                //        if (taskConfig == null)
                //            return;
                //        if (eCA.PageSource.IsNullOrEmpty())
                //            return;
                //        string noResults = string.Empty;
                //        if (taskConfig.ContainsKey("EX_NoResults"))
                //        {
                //            noResults=eCA.PageSource.RegexMatchString(taskConfig.GetParamValueByKey("EX_NoResults")).GetText();
                //        }
                //        txtRPageSource.Text = noResults.IsNullOrEmpty() 
                //            ? eCA.PageSource.RegexMatchHtmlTag(taskConfig.GetParamValueByKey("EX_Results")) 
                //            : noResults;
                //    }
                //};
                //crawlService.StartCrawl(taskItem, websiteConfig, websiteConfig.Timeout, new TimeSpan(0, 3, 0));
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
                //CrawlConfigParam param1 = new CrawlConfigParam
                //{
                //    KeyType = Website_KeyType.FormElement,
                //    KeyValue = "http://state.jingtum.com/#!/wallet/jfQkiRE3MrrMfUxwsf5TLoLUvo9gnFkWKE",
                //    KeyValueType = Website_KeyValueType.Navigate ,
                //    Timeout = 60,
                //    SortIndex = 1 ,
                //};
                ////CrawlConfigParam param2 = new CrawlConfigParam
                //{
                //    KeyType = Website_KeyType.FormElement,
                //    KeyValue = "<tr[^>]+ledger_item[^>]+>",
                //    KeyValueType = Website_KeyValueType.Wait,
                //    ParamType = Website_ParamType.Constants,
                //    ParamValue = "NoData" ,
                //    Timeout = 10,
                //    SortIndex = 2,
                //};

                //CrawlConfig webConfig = new CrawlConfig
                //{
                //    ID = "0C132221-573C-437F-82D5-FA61F94CAB5A".NewGuid(),
                //    Timeout = 60 ,
                //    WebsiteParams = new List<CrawlConfigParam>()
                //    {
                //       param1, //param2 , 
                //    },
                //};


                //IWCrawler wCrawler = new WCrawler("0C132221-573C-437F-82D5-FA61F94CAB5A")
                //{
                //    StartAction = (paramStart) =>
                //    {
                //        CrawlConfig taskConfig = paramStart.TaskConfig as CrawlConfig;
                //        paramStart.WebDriver.ExecActionByWebConfig((object)paramStart.TaskObject, taskConfig);
                //    },
                //    ErrorAction = (paramError) =>
                //    {
                //        txtRError.Text = paramError.Exception.GetBaseException().Message;
                //    },
                //    CompletedAction= (paramComplete) =>
                //    {
                //        string pageSource = paramComplete.PageSource;

                //        string strFormat = INIHelper.Instance.IniReadValue("JT", "Format1");
                //        MatchCollection mc= pageSource.RegexMatchesSingleline(strFormat);
                //        StringBuilder strResults = new StringBuilder();
                //        foreach (Match m in mc)
                //        {
                //            strResults.AppendFormat("Href:{0} Ledger:{1} Value:{2} \r\n", m.Groups["hash_href"].Value, m.Groups["hash_ledger"].Value, m.Groups["hash_value"].Value);
                //        }
                //        Console.WriteLine(strResults.ToString());
                //    },
                //    StopAction = (paramStop) =>
                //    {
                        
                //    },
                //};
                //wCrawler.StartCrawl(null, webConfig, webConfig.Timeout, new TimeSpan(8, 0, 0));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

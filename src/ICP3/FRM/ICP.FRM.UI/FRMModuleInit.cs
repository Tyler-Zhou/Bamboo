#region Comment

/*
 * 
 * FileName:    FRMModuleInit.cs
 * CreatedOn:   
 * CreatedBy:   
 * 
 * 
 * Description：
 *      ->FRM初始化
 * History：
 *      ->
 * 
 * 
 * 
 */

#endregion

namespace ICP.FRM.UI
{
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.CompositeUI.Commands;
    using OceanPrice;
    using System;
    using Framework.CommonLibrary.Client;
    using ServiceInterface;
    using ClientService;
    using InquireRates;
    using ICP.Framework.ClientComponents.Service;
    using ICP.FRM.UI.SearchRate;

    /// <summary>
    /// FRM初始化
    /// </summary>
    public class FRMModuleInit : ModuleInit
    {
        #region 成员变量
        /// <summary>
        /// FRM界面容器
        /// </summary>
        WorkItem _rootWorkItem;

        IDataFinderFactory _datafinderFactory; 
        #endregion

        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rootWorkItem"></param>
        /// <param name="datafinderFactory"></param>
        public FRMModuleInit([ServiceDependency]WorkItem rootWorkItem
            , [ServiceDependency]IDataFinderFactory datafinderFactory
            )
        {
            _rootWorkItem = rootWorkItem;
            _datafinderFactory = datafinderFactory;
        } 
        #endregion

        #region Override
        /// <summary>
        /// 添加服务
        /// </summary>
        public override void AddServices()
        {
            //海运文件服务
            _rootWorkItem.Services.AddNew<OceanPriceFileClientService, IOceanPriceFileClientService>();
            //询价客户端服务类
            _rootWorkItem.Services.AddNew<ClientInquireRateService, IClientInquireRateService>();
            //询价邮件服务类
            _rootWorkItem.Services.AddNew<InquireRateEmailService, IInquireRateEmailService>();
            base.AddServices();
        } 
        #endregion

        #region Command
        /// <summary>
        /// 打开海运运价管理
        /// </summary>
        [CommandHandler(FunctionConstants.FRM_OCEANPRICELIST)]
        public void Open_FRM_OCEANPRICELIST(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm();

            OceanPriceWorkitem oceanPriceWorkitem = _rootWorkItem.WorkItems.AddNew<OceanPriceWorkitem>();
            try
            {
                oceanPriceWorkitem.Run();
            }
            catch { oceanPriceWorkitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }

        /// <summary>
        /// 打开商务周报表列表
        /// </summary>
        [CommandHandler(FunctionConstants.FRM_BUSINESSWEEKLYREPORT)]
        public void Open_FRM_BUSINESSWEEKLYREPORT(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            BusinessWeeklyReportWoritem bwpWorkitem = _rootWorkItem.WorkItems.AddNew<BusinessWeeklyReportWoritem>();
            bwpWorkitem.Run();
            LoadingServce.CloseLoadingForm(theradID);
        }


        /// <summary>
        /// 查询运价
        /// </summary>
        [CommandHandler(FunctionConstants.FRM_SEARCHRATE)]
        public void Open_FRM_SEARCHRATE(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");

            SearchRateWoritem searchRateWorkitem = _rootWorkItem.WorkItems.AddNew<SearchRateWoritem>();
            try
            {
                searchRateWorkitem.Run();
            }
            catch (Exception) { searchRateWorkitem.Dispose(); }


            #region
            //string fileName = Application.StartupPath + "\\TestSearchConfig.xml";
            //System.Xml.Linq.XDocument doc = System.Xml.Linq.XDocument.Load(fileName);


            //if (((System.Xml.Linq.XElement)(doc.FirstNode)).Value.Trim() == "1")
            //{
            //    SearchRate.SearchRateWoritem searchRateWorkitem = _rootWorkItem.WorkItems.AddNew<SearchRate.SearchRateWoritem>();
            //    try
            //    {
            //        searchRateWorkitem.Run();
            //    }
            //    catch (Exception) { searchRateWorkitem.Dispose(); }
            //}
            //else if (((System.Xml.Linq.XElement)(doc.FirstNode)).Value.Trim() == "2")
            //{

            //    SearchRate.SearchAirWorkitem searchRateWorkitem = _rootWorkItem.WorkItems.AddNew<SearchRate.SearchAirWorkitem>();
            //    try
            //    {
            //        searchRateWorkitem.Run();
            //    }
            //    catch (Exception) { searchRateWorkitem.Dispose(); }
            //}
            //else if (((System.Xml.Linq.XElement)(doc.FirstNode)).Value.Trim() == "3")
            //{
            //    SearchRate.SearchTruckWorkitem searchRateWorkitem = _rootWorkItem.WorkItems.AddNew<SearchRate.SearchTruckWorkitem>();
            //    try
            //    {
            //        searchRateWorkitem.Run();
            //    }
            //    catch (Exception) { searchRateWorkitem.Dispose(); }
            //}
            #endregion
            LoadingServce.CloseLoadingForm(theradID);
        }


        /// <summary>
        /// 打开询价
        /// </summary>
        [CommandHandler(FunctionConstants.FRM_InquireRates)]
        public void Open_FRM_InquireRates(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            InquireRatesWorkitem inquireRatesWorkitem = _rootWorkItem.WorkItems.AddNew<InquireRatesWorkitem>();
            try
            {
                inquireRatesWorkitem.Run();
            }
            catch (Exception ex) { inquireRatesWorkitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }

        /// <summary>
        /// 打开订舱统计
        /// </summary>
        [CommandHandler(FunctionConstants.FRM_BookingReport)]
        public void Open_FRM_BookingReport(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            BookingReportWoritem bookingReportWorkitem = _rootWorkItem.WorkItems.AddNew<BookingReportWoritem>();
            try
            {
                bookingReportWorkitem.Run();
            }
            catch (Exception ex) { bookingReportWorkitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
        }

        /// <summary>
        /// 打开订舱统计
        /// </summary>
        [CommandHandler(FunctionConstants.FRM_ProfitRatios)]
        public void Open_FRM_ProfitRatios(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            PRWorkitem prWorkItem = _rootWorkItem.WorkItems.AddNew<PRWorkitem>();
            try
            {
                prWorkItem.Run();
            }
            catch (Exception ex)
            {

                prWorkItem.Dispose();
            }

            LoadingServce.CloseLoadingForm(theradID);
        }
        #endregion

    }
}

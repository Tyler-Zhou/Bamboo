using System;
using ICP.Business.Common.UI.QuotedPrice;
using ICP.Framework.ClientComponents.Service;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Business.Common.UI.CSPBooking;
using ICP.FCM.Common.ServiceInterface.Common;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.Business.Common.UI
{
    /// <summary>
    /// 通用业务模块初始化
    /// </summary>
    public class BusinessCommonModuleInit : ModuleInit
    {
        /// <summary>
        /// WorkItem
        /// </summary>
        WorkItem WorkItemModule;

        /// <summary>
        /// 通用业务模块初始化:构造函数
        /// </summary>
        /// <param name="workitemmodule"></param>
        public BusinessCommonModuleInit([ServiceDependency]WorkItem workitemmodule)
        {
            WorkItemModule = workitemmodule;
        }

        #region Client Service Register

        /// <summary>
        /// 添加服务
        /// </summary>
        public override void AddServices()
        {
            base.AddServices();
            //TODO:添加新服务
        }

        #endregion

        /// <summary>
        /// 报价列表命令名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(BusinessCommandConstants.QuotedPrice_List)]
        public void Open_OceanExport_QPList(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");

            QuotedPriceWorkItem WorkItemTabPage = WorkItemModule.WorkItems.AddNew<QuotedPriceWorkItem>();
            try
            {
                WorkItemTabPage.Run();
            }
            catch
            {
                WorkItemTabPage.Dispose();
            }
            finally
            {
                LoadingServce.CloseLoadingForm(theradID);
            }
        }

        /// <summary>
        /// CSPBooking下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(FCMCommonContants.COMMAND_CSPBOOKING_DOWNLOAD)]
        public void Open_CSPBooking_Download(object sender, EventArgs e)
        {
            int theradID = 0;
            string titlename = LocalData.IsEnglish ? "CSP Booking" : "CSP订舱单";
            theradID = LoadingServce.ShowLoadingForm(titlename);

            CSPBKWorkItem WorkItemTabPage = WorkItemModule.WorkItems.AddNew<CSPBKWorkItem>();
            try
            {
                WorkItemTabPage.TitleName = titlename; 
                WorkItemTabPage.Run();
            }
            catch
            {
                WorkItemTabPage.Dispose();
            }
            finally
            {
                LoadingServce.CloseLoadingForm(theradID);
            }
        }
    }
}

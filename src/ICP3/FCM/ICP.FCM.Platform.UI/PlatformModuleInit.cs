using ICP.FCM.Platform.UI.Common;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using System;

namespace ICP.FCM.Platform.UI
{
    public class PlatformModuleInit : ModuleInit
    {
        /// <summary>
        /// WorkItem
        /// </summary>
        WorkItem _rootWorkItem;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="rootWorkItem"></param>
        public PlatformModuleInit([ServiceDependency]WorkItem rootWorkItem)
        {
            _rootWorkItem = rootWorkItem;
        }

        #region AddServices

        /// <summary>
        /// 
        /// </summary>
        public override void AddServices()
        {
            base.AddServices();
            IDataFinderFactory datafinderFactory = ServiceClient.GetClientService<IDataFinderFactory>();
            //_rootWorkItem.Services.AddNew<ClientOceanExportService, IClientOceanExportService>();
        }

        #endregion

        /// <summary>
        /// CSP 订单列表
        /// </summary>
        [CommandHandler(CommandConstants.PLATFORM_CSPBOOKING_LIST)]
        public void Open_CSPBooking_List(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");

            //OEBLWorkitem blWorkitem = _rootWorkItem.WorkItems.AddNew<OEBLWorkitem>();
            try
            {
                //blWorkitem.Run();
            }
            catch
            {
                //blWorkitem.Dispose();
            }
            finally
            {
                LoadingServce.CloseLoadingForm(theradID);
            }
        }
    }
}

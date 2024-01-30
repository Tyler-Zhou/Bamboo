using System;
using ICP.FCM.AirExport.ServiceInterface;
using ICP.FCM.AirExport.ServiceInterface.Comm;
using ICP.FCM.AirExport.UI.BL;
using ICP.FCM.AirExport.UI.Booking;
using ICP.FCM.AirExport.UI.Booking.Finder;
using ICP.FCM.AirExport.UI.BusinessInfoProvider;
using ICP.FCM.AirExport.UI.Order;
using ICP.Framework.ClientComponents.Service;
using Microsoft.Practices.CompositeUI;

using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FCM.AirExport.UI
{
    public class AirExportModuleInit : ModuleInit
    {

        WorkItem _rootWorkItem;
        

        public AirExportModuleInit([ServiceDependency]WorkItem rootWorkItem
            )
        {
            _rootWorkItem = rootWorkItem;
        }

        #region DataFinder Register

        /// <summary>
        /// TODO: 公司和默认部门的必配要求不能放在这里。
        /// </summary>
        public override void AddServices()
        {
            base.AddServices();
            //AirBooking
            IDataFinderFactory datafinderFactory = ServiceClient.GetClientService<IDataFinderFactory>();
            datafinderFactory.Register<OEBookingFinder>
                (FCMFinderConstants.AirBookingFinder);
            IBusinessInfoProviderFactory businessInfoProviderFactory = ServiceClient.GetClientService<IBusinessInfoProviderFactory>();
            businessInfoProviderFactory.Register<OEBusinessInfoProvider>
                (OperationType.AirExport);

            _rootWorkItem.Services.AddNew<ClientAirExportService, IClientAirExportService>();
        }

        #endregion

        //提单列表命令名
        [CommandHandler(CommandConstants.FCM_AE_BLLIST)]
        public void Open_AirExport_BLList(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID=LoadingServce.ShowLoadingForm("Loading...");
            AEBLWorkitem blWorkitem = _rootWorkItem.WorkItems.AddNew<AEBLWorkitem>();
            blWorkitem.Run();
            LoadingServce.CloseLoadingForm(theradID);
        }

        //订舱列表命令名
        [CommandHandler(CommandConstants.FCM_AE_BOOKINGLIST)]
        public void Open_AirExport_BookingList(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID=LoadingServce.ShowLoadingForm("Loading...");
            BookingWorkitem bookingWorkitem = _rootWorkItem.WorkItems.AddNew<BookingWorkitem>();
            bookingWorkitem.Run();
            LoadingServce.CloseLoadingForm(theradID);
        }

        //订单列表命令名
        [CommandHandler(CommandConstants.FCM_AE_ORDERLIST)]
        public void Open_AirExport_OrderList(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID=LoadingServce.ShowLoadingForm("Loading...");
            OrderWorkitem orderWorkitem = _rootWorkItem.WorkItems.AddNew<OrderWorkitem>();
            orderWorkitem.Run();
            LoadingServce.CloseLoadingForm(theradID);
        }
    }
}

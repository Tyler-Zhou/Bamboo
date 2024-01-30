using System;
using ICP.FCM.DomesticTrade.ServiceInterface.Comm;
using ICP.FCM.DomesticTrade.UI.Booking;
using ICP.FCM.DomesticTrade.UI.Booking.Finder;
using ICP.FCM.DomesticTrade.UI.BusinessInfoProvider;
using ICP.FCM.DomesticTrade.UI.Order;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FCM.DomesticTrade.UI
{
    public class DomesticTradeModuleInit : ModuleInit
    {

        WorkItem _rootWorkItem;


        public DomesticTradeModuleInit([ServiceDependency]WorkItem rootWorkItem
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
            IDataFinderFactory datafinderFactory = ServiceClient.GetClientService<IDataFinderFactory>();
            
            datafinderFactory.Register<DTBookingFinder>
                (FCMFinderConstants.DTBookingFinder);
            IBusinessInfoProviderFactory businessInfoProviderFactory = ServiceClient.GetClientService<IBusinessInfoProviderFactory>();
            businessInfoProviderFactory.Register<DTBusinessInfoProvider>
                (OperationType.Internal); 
        }

        #endregion

       

        //订舱列表命令名
        [CommandHandler(CommandConstants.DOMESTICTRADE_BOOKINGLIST)]
        public void Open_DomesticTrade_BookingList(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID=LoadingServce.ShowLoadingForm("Loading...");
            BookingWorkitem bookingWorkitem = _rootWorkItem.WorkItems.AddNew<BookingWorkitem>();

            try
            {
                bookingWorkitem.Run();
            }
            catch { bookingWorkitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
            
        }

        //联单列表命令名
        [CommandHandler(CommandConstants.DOMESTICTRADE_ORDERLIST)]
        public void Open_DomesticTrade_OrderList(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID=LoadingServce.ShowLoadingForm("Loading...");

            DTOrderWorkitem orderWorkitem = _rootWorkItem.WorkItems.AddNew<DTOrderWorkitem>();
            try
            {
                orderWorkitem.Run();
            }
            catch { orderWorkitem.Dispose(); }

            LoadingServce.CloseLoadingForm(theradID);
          
            
        }

    }
}

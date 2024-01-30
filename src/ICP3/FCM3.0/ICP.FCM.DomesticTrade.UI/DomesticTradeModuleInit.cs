using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Server;

using Microsoft.Practices.CompositeUI.Commands;
using ICP.Sys.ServiceInterface;
using ICP.FCM.DomesticTrade.ServiceInterface.Comm;
using ICP.FCM.DomesticTrade.ServiceInterface;
using ICP.FCM.DomesticTrade.ServiceInterface.DataObjects;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.FAM.ServiceInterface;

namespace ICP.FCM.DomesticTrade.UI
{
    public class DomesticTradeModuleInit : Microsoft.Practices.CompositeUI.ModuleInit
    {

        Microsoft.Practices.CompositeUI.WorkItem _rootWorkItem;
        IDataFinderFactory _datafinderFactory;
        IBusinessInfoProviderFactory _businessInfoProviderFactory;

        public DomesticTradeModuleInit([ServiceDependency]WorkItem rootWorkItem
            , [ServiceDependency]IDataFinderFactory datafinderFactory
            , [ServiceDependency]IBusinessInfoProviderFactory businessInfoProviderFactory
            )
        {
            _rootWorkItem = rootWorkItem;
            _datafinderFactory = datafinderFactory;
            _businessInfoProviderFactory = businessInfoProviderFactory;
        }

        #region DataFinder Register

        [ServiceDependency]
        public IDomesticTradeService oeService { get; set; }

        [ServiceDependency]
        public ICP.Sys.ServiceInterface.IUserService userService { get; set; }

        /// <summary>
        /// TODO: 公司和默认部门的必配要求不能放在这里。
        /// </summary>
        public override void AddServices()
        {
            base.AddServices();

            //Utility.EnsureDefaultCompanyExists(this.userService);
            //Utility.EnsureDefaultDepartmentExists(this.userService);

            _datafinderFactory.Register<ICP.FCM.DomesticTrade.UI.Booking.Finder.DTBookingFinder>
                (ICP.FCM.DomesticTrade.ServiceInterface.Comm.FCMFinderConstants.DTBookingFinder);

            _businessInfoProviderFactory.Register<ICP.FCM.DomesticTrade.UI.BusinessInfoProvider.DTBusinessInfoProvider>
                (OperationType.Internal); 
        }

        #endregion

       

        //订舱列表命令名        [CommandHandler(CommandConstants.DOMESTICTRADE_BOOKINGLIST)]
        public void Open_DomesticTrade_BookingList(object sender, EventArgs e)
        {
            ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm("Loading...");
            Booking.BookingWorkitem bookingWorkitem = _rootWorkItem.WorkItems.AddNew<Booking.BookingWorkitem>();

            try
            {
                bookingWorkitem.Run();
            }
            catch { bookingWorkitem.Dispose(); }

            ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm();
            
        }

        //联单列表命令名
        [CommandHandler(CommandConstants.DOMESTICTRADE_ORDERLIST)]
        public void Open_DomesticTrade_OrderList(object sender, EventArgs e)
        {
            ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm("Loading...");

            Order.DTOrderWorkitem orderWorkitem = _rootWorkItem.WorkItems.AddNew<Order.DTOrderWorkitem>();
            try
            {
                orderWorkitem.Run();
            }
            catch { orderWorkitem.Dispose(); }

            ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm();
          
            
        }

    }
}

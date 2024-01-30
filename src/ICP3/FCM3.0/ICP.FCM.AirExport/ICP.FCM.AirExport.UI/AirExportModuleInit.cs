using System;


using Microsoft.Practices.CompositeUI;

using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FCM.AirExport.UI
{
    public class AirExportModuleInit : Microsoft.Practices.CompositeUI.ModuleInit
    {

        Microsoft.Practices.CompositeUI.WorkItem _rootWorkItem;
        IDataFinderFactory _datafinderFactory;
        IBusinessInfoProviderFactory _businessInfoProviderFactory;

        public AirExportModuleInit([ServiceDependency]WorkItem rootWorkItem
            , [ServiceDependency]IDataFinderFactory datafinderFactory
            , [ServiceDependency]IBusinessInfoProviderFactory businessInfoProviderFactory
            )
        {
            _rootWorkItem = rootWorkItem;
            _datafinderFactory = datafinderFactory;
            _businessInfoProviderFactory = businessInfoProviderFactory;
        }

        #region DataFinder Register

        /// <summary>
        /// TODO: 公司和默认部门的必配要求不能放在这里。
        /// </summary>
        public override void AddServices()
        {
            base.AddServices();

            //Utility.EnsureDefaultCompanyExists(this.userService);
            //Utility.EnsureDefaultDepartmentExists(this.userService);

            //AirBooking
            _datafinderFactory.Register<ICP.FCM.AirExport.UI.Booking.Finder.OEBookingFinder>
                (ICP.FCM.AirExport.ServiceInterface.Comm.FCMFinderConstants.AirBookingFinder);

            _businessInfoProviderFactory.Register<ICP.FCM.AirExport.UI.BusinessInfoProvider.OEBusinessInfoProvider>
                (OperationType.AirExport);
        }

        #endregion

        //提单列表命令名
        [CommandHandler(CommandConstants.FCM_AE_BLLIST)]
        public void Open_AirExport_BLList(object sender, EventArgs e)
        {
            BL.AEBLWorkitem blWorkitem = _rootWorkItem.WorkItems.AddNew<BL.AEBLWorkitem>();
            blWorkitem.Run();
        }

        //订舱列表命令名
        [CommandHandler(CommandConstants.FCM_AE_BOOKINGLIST)]
        public void Open_AirExport_BookingList(object sender, EventArgs e)
        {
            Booking.BookingWorkitem bookingWorkitem = _rootWorkItem.WorkItems.AddNew<Booking.BookingWorkitem>();
            bookingWorkitem.Run();
        }

        //订单列表命令名
        [CommandHandler(CommandConstants.FCM_AE_ORDERLIST)]
        public void Open_AirExport_OrderList(object sender, EventArgs e)
        {
            Order.OrderWorkitem orderWorkitem = _rootWorkItem.WorkItems.AddNew<Order.OrderWorkitem>();
            orderWorkitem.Run();
        }
    }
}

using System;


using Microsoft.Practices.CompositeUI;

using Microsoft.Practices.CompositeUI.Commands;
using ICP.Framework.CommonLibrary.Client;
//using ICP.FCM.OceanExport.UI.ClientService;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.Framework.CommonLibrary.Common;
//using ICP.Framework.CommonLibrary.Common;
//using ICP.FCM.OceanExport.UI.ClientService;

namespace ICP.FCM.OceanExport.UI
{
    public class OceanExportModuleInit : Microsoft.Practices.CompositeUI.ModuleInit
    {

        Microsoft.Practices.CompositeUI.WorkItem _rootWorkItem;
        IDataFinderFactory _datafinderFactory;
        IBusinessInfoProviderFactory _businessInfoProviderFactory;

        public OceanExportModuleInit([ServiceDependency]WorkItem rootWorkItem
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


            //_rootWorkItem.Services.AddNew<WorkOnOceanExportClientService, IWorkOnOceanExportClientService>();

            //OceanBooking
            _datafinderFactory.Register<ICP.FCM.OceanExport.UI.Booking.Finder.OEBookingFinder>
                (ICP.FCM.OceanExport.ServiceInterface.Comm.FCMFinderConstants.OceanBookingFinder);

            _businessInfoProviderFactory.Register<ICP.FCM.OceanExport.UI.BusinessInfoProvider.OEBusinessInfoProvider>
                (OperationType.OceanExport);
        }

        #endregion

        //提单列表命令名
        [CommandHandler(CommandConstants.OceanExport_BLList)]
        public void Open_OceanExport_BLList(object sender, EventArgs e)
        {
            ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm("Loading...");
            BL.OEBLWorkitem blWorkitem = _rootWorkItem.WorkItems.AddNew<BL.OEBLWorkitem>();
            try
            {
                blWorkitem.Run();
            }
            catch { blWorkitem.Dispose(); }

            ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm();
        }

        //订舱列表命令名
        [CommandHandler(CommandConstants.OceanExport_BookingList)]
        public void Open_OceanExport_BookingList(object sender, EventArgs e)
        {
            ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm("Loading...");
            Booking.BookingWorkitem bookingWorkitem = _rootWorkItem.WorkItems.AddNew<Booking.BookingWorkitem>();

            try
            {
                bookingWorkitem.Run();
            }
            catch(Exception ex) 
            {
                Utility.ShowMessage(ex.Message);
                bookingWorkitem.Dispose(); }

            ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm();
           
        }

        //联单列表命令名
        [CommandHandler(CommandConstants.OceanExport_OrderList)]
        public void Open_OceanExport_OrderList(object sender, EventArgs e)
        {
            ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm("Loading...");
            
            Order.OEOrderWorkitem orderWorkitem = _rootWorkItem.WorkItems.AddNew<Order.OEOrderWorkitem>();
            try
            {
                orderWorkitem.Run();
            }
            catch(Exception ex)
            {
                Utility.ShowMessage(ex.Message);
                orderWorkitem.Dispose();
            }

            ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm();

        }

    }
}

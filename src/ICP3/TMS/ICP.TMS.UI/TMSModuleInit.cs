namespace ICP.TMS.UI
{
    using Microsoft.Practices.CompositeUI;
    using Microsoft.Practices.CompositeUI.Commands;
    using System;

    public class TMSModuleInit : ModuleInit
    {
        Microsoft.Practices.CompositeUI.WorkItem _rootWorkItem;

        public TMSModuleInit([ServiceDependency]WorkItem rootWorkItem
            )
        {
            _rootWorkItem = rootWorkItem;
        }

        public override void AddServices()
        {

            base.AddServices();
        }

        //拖车业务界面
        [CommandHandler(FunctionConstants.TMS_TRUCKBUSINESS)]
        public void Open_TMS_TRUCKBUSINESS(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID=ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm();

            TruckBookingsWorkitem truckBusinessWorkitem = _rootWorkItem.WorkItems.AddNew<TruckBookingsWorkitem>();
            try
            {
                truckBusinessWorkitem.Run();
            }
            catch { truckBusinessWorkitem.Dispose(); }

            ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(theradID);
        }



        //司机
        [CommandHandler(FunctionConstants.TMS_DRIVERLIST)]
        public void Open_TMS_DRIVERLIST(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID=ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm();

            DriverWorkItem driverWorkItem = _rootWorkItem.WorkItems.AddNew<DriverWorkItem>();
            try
            {
                driverWorkItem.Run();
            }
            catch { driverWorkItem.Dispose(); }

            ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(theradID);
        }


        //拖车
        [CommandHandler(FunctionConstants.TMS_TRUCKLIST)]
        public void Open_TMS_TRUCKLIST(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID=ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm();

            TruckWorkItem truckWorkItem = _rootWorkItem.WorkItems.AddNew<TruckWorkItem>();
            try
            {
                truckWorkItem.Run();
            }
            catch { truckWorkItem.Dispose(); }

            ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm(theradID);
        }

    }

}

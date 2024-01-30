using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Server;
using Microsoft.Practices.CompositeUI.Commands;
using ICP.Sys.ServiceInterface;
using ICP.FCM.OtherBusiness.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.FAM.ServiceInterface;
using ICP.FCM.OtherBusiness.UI.Business;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;

namespace ICP.FCM.OtherBusiness.UI
{
    public class OtherBusinessModuleInit : Microsoft.Practices.CompositeUI.ModuleInit
    {
        
        Microsoft.Practices.CompositeUI.WorkItem _rootWorkItem;
        IDataFinderFactory _datafinderFactory;
        IBusinessInfoProviderFactory _businessInfoProviderFactory;

        public OtherBusinessModuleInit([ServiceDependency]WorkItem rootWorkItem
            , [ServiceDependency]IDataFinderFactory datafinderFactory
            , [ServiceDependency]IBusinessInfoProviderFactory businessInfoProviderFactory
            )
        {
            _rootWorkItem = rootWorkItem;
            _datafinderFactory = datafinderFactory;
            _businessInfoProviderFactory = businessInfoProviderFactory;
        }

        #region DataFinder Register

        //[ServiceDependency]
        //public IAirExportService aeService { get; set; }

        [ServiceDependency]
        public ICP.Sys.ServiceInterface.IUserService userService { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public override void AddServices()
        {
            base.AddServices();

            //Utility.EnsureDefaultCompanyExists(this.userService);
            //Utility.EnsureDefaultDepartmentExists(this.userService);

        }

        #endregion

        //其他业务列表
        [CommandHandler(CommandConstants.FCM_OTHERBUSINESS)]
        public void Open_OB_BUSINESSLIST(object sender, EventArgs e)
        {
            ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm("Loading...");
            Business.OBWorkitem obWorkItem = _rootWorkItem.WorkItems.AddNew<Business.OBWorkitem>();
            try
            {
                obWorkItem.Run();
            }
            catch { obWorkItem.Dispose(); }
            ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm();
        }

        //其他业务订单列表(查询时不显示业务号）
        [CommandHandler(CommandConstants.FCM_OTHERORDER)]
        public void Open_OB_BUSINESSList(object sender, EventArgs e)
        {
            ICP.Framework.ClientComponents.Service.LoadingServce.ShowLoadingForm("Loading...");
            Business.OBBookingWorkitem obWorkItem1 = _rootWorkItem.WorkItems.AddNew<Business.OBBookingWorkitem>();
            try
            {
                obWorkItem1.Run();
            }
            catch { obWorkItem1.Dispose(); }
            ICP.Framework.ClientComponents.Service.LoadingServce.CloseLoadingForm();
        }

        //订单列表命令名
        [CommandHandler(CommandConstants.FCM_AE_ORDERLIST)]
        public void Open_OB_OrderList(object sender, EventArgs e)
        {
            //Order.OrderWorkitem orderWorkitem = _rootWorkItem.WorkItems.AddNew<Order.OrderWorkitem>();
            //orderWorkitem.Run();
        }
    }
}

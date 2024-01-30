using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface.Comm;
using ICP.FCM.OceanExport.UI.BL;
using ICP.FCM.OceanExport.UI.Booking;
using ICP.FCM.OceanExport.UI.Booking.Finder;
using ICP.FCM.OceanExport.UI.BusinessInfoProvider;
using ICP.FCM.OceanExport.UI.Order;
using ICP.Framework.ClientComponents;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using System;

namespace ICP.FCM.OceanExport.UI
{
    /// <summary>
    /// 海运出口模块入口
    /// </summary>
    public class OceanExportModuleInit : ModuleInit
    {
        /// <summary>
        /// WorkItem
        /// </summary>
        WorkItem _rootWorkItem;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="rootWorkItem"></param>
        public OceanExportModuleInit([ServiceDependency]WorkItem rootWorkItem)
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

            datafinderFactory.Register<OEBookingFinder>
                (FCMFinderConstants.OceanBookingFinder);
            IBusinessInfoProviderFactory businessInfoProviderFactory = ServiceClient.GetClientService<IBusinessInfoProviderFactory>();
            businessInfoProviderFactory.Register<OEBusinessInfoProvider>
                (OperationType.OceanExport);

            _rootWorkItem.Services.AddNew<ClientOceanExportService, IClientOceanExportService>();
        }

        #endregion

        /// <summary>
        /// 提单列表命令名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.OceanExport_BLList)]
        public void Open_OceanExport_BLList(object sender, EventArgs e)
        {
            if (IsDisablePart())
                return;

            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");

            OEBLWorkitem blWorkitem = _rootWorkItem.WorkItems.AddNew<OEBLWorkitem>();
            try
            {
                blWorkitem.Run();
            }
            catch
            {
                blWorkitem.Dispose();
            }
            finally
            {
                LoadingServce.CloseLoadingForm(theradID);
            }
        }




        /// <summary>
        /// 订舱列表命令名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.OceanExport_BookingList)]
        public void Open_OceanExport_BookingList(object sender, EventArgs e)
        {
            if (IsDisablePart())
                return;
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            WorkItemBooking WorkItemTabPage = _rootWorkItem.WorkItems.AddNew<WorkItemBooking>();
            try
            {
                WorkItemTabPage.Run();
            }
            catch (Exception ex)
            {
                MessageBoxService.ShowInfo(ex.Message);
                WorkItemTabPage.Dispose();
            }
            finally
            {
                LoadingServce.CloseLoadingForm(theradID);
            }

        }

        /// <summary>
        /// 联单列表命令名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.OceanExport_OrderList)]
        public void Open_OceanExport_OrderList(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");

            OEOrderWorkitem orderWorkitem = _rootWorkItem.WorkItems.AddNew<OEOrderWorkitem>();
            try
            {
                orderWorkitem.Run();
            }
            catch (Exception ex)
            {
                MessageBoxService.ShowInfo(ex.Message);
                orderWorkitem.Dispose();
            }
            finally
            {
                LoadingServce.CloseLoadingForm(theradID);
            }

        }
        /// <summary>
        /// 是否禁用面板
        /// </summary>
        /// <returns></returns>
        public bool IsDisablePart()
        {
            string tip = string.Empty;
            if (LocalData.ShieldNo == 1)
            {
                if (LocalData.UserInfo.DefaultDepartmentID.ToString().ToUpper() ==
                    "EBFE6FDD-800F-436B-B2B3-8DE85039B15E")
                {
                    return false;
                }
                if (
                (LocalData.UserInfo.DefaultCompanyID.ToString().ToUpper() == "41D7D3FE-183A-41CD-A725-EB6F728541EC" ||
                 LocalData.UserInfo.DefaultCompanyID.ToString().ToUpper() == "2B109BA9-D770-419D-9323-34EE1553FC2E"))
                {
                    tip = LocalData.IsEnglish
                        ? "This page is migrated to [OA/Task Center], any question please contact to IT Dept."
                        : "功能已经移到[办公自动化>任务中心]，如有问题请咨询IT部Tom Lai.";

                    MessageBoxService.ShowInfo(tip);
                    return true;
                }
                tip = LocalData.IsEnglish
                    ? "[Ocean Import & Export] will be disable on 2015/12/15, insteading of the functionality is [OA]/[Task Center], please learn it early."
                    : "[海运进出口查询页面]将在2015/12/15 禁用，此功能已由[办公自动化]/[任务中心]代替，请作好提前学习。";

                MessageBoxService.ShowInfo(tip);
                return false;
            }
            return false;
        }

    }
}

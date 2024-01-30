using ICP.FCM.OtherBusiness.ServiceInterface;
using ICP.FCM.OtherBusiness.UI.Business;
using ICP.FCM.OtherBusiness.UI.ECommerce;
using ICP.FCM.OtherBusiness.UI.Order;
using ICP.Framework.ClientComponents.Service;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Commands;
using System;

namespace ICP.FCM.OtherBusiness.UI
{
    /// <summary>
    /// 其他业务模块初始化
    /// </summary>
    public class OtherBusinessModuleInit : ModuleInit
    {
        /// <summary>
        /// WorkItem
        /// </summary>
        WorkItem _rootWorkItem;
        /// <summary>
        /// 其他业务模块初始化
        /// </summary>
        /// <param name="rootWorkItem"></param>
        public OtherBusinessModuleInit([ServiceDependency]WorkItem rootWorkItem)
        {
            _rootWorkItem = rootWorkItem;
        }
        /// <summary>
        /// 添加服务
        /// </summary>
        public override void AddServices()
        {
            base.AddServices();
            _rootWorkItem.Services.AddNew<ClientOtherBusinessService, IClientOtherBusinessService>();
        }
        /// <summary>
        /// 其他业务列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.FCM_OTHERBUSINESS)]
        public void Open_OB_BUSINESSLIST(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID=LoadingServce.ShowLoadingForm("Loading...");
            OBBWorkitem obWorkItem = _rootWorkItem.WorkItems.AddNew<OBBWorkitem>();
            try
            {
                obWorkItem.Run();
            }
            catch { obWorkItem.Dispose(); }
            LoadingServce.CloseLoadingForm(theradID);
        }

        /// <summary>
        /// 其他业务订单列表(查询时不显示业务号）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.FCM_OTHERORDER)]
        public void Open_OB_BUSINESSList(object sender, EventArgs e)
        {
            int theradID = 0;

            theradID=LoadingServce.ShowLoadingForm("Loading...");
            OBOrderWorkitem OBOWorkItem = _rootWorkItem.WorkItems.AddNew<OBOrderWorkitem>();
            try
            {
                OBOWorkItem.Run();
            }
            catch { OBOWorkItem.Dispose(); }
            LoadingServce.CloseLoadingForm(theradID);
        }

        /// <summary>
        /// 其他业务-电商物流
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [CommandHandler(CommandConstants.FCM_OTHERBUSINESS_ECOMMERCE)]
        public void Open_OB_ECommerce(object sender, EventArgs e)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            OBECWorkItem obECWorkItem = _rootWorkItem.WorkItems.AddNew<OBECWorkItem>();
            try
            {
                obECWorkItem.Run();
            }
            catch { obECWorkItem.Dispose(); }
            LoadingServce.CloseLoadingForm(theradID);
        }
    }
}

#region Comment

/*
 * 
 * FileName:    InquireRatesWorkitem.cs
 * CreatedOn:   
 * CreatedBy:   
 * 
 * 
 * Description：
 *      ->询价界面运行容器
 *      ->1.初始化询价界面布局及其事件
 * History：
 *      ->
 * 
 * 
 * 
 */

#endregion

using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI;
using System.ComponentModel;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace ICP.FRM.UI.InquireRates
{
    /// <summary>
    /// 询价界面容器
    /// </summary>
    public class InquireRatesWorkitem : WorkItem
    {
        #region Override
        /// <summary>
        /// 开始运行后
        /// </summary>
        protected override void OnRunStarted()
        {
            base.OnRunStarted();
            Show();
        } 
        #endregion

        #region Show Part
        /// <summary>
        /// 显示面板
        /// </summary>
        private void Show()
        {
            InquireRatesMainWorkspace irMainWorkspace = SmartParts.Get<InquireRatesMainWorkspace>("InquireRatesMainWorkspace");
            if (irMainWorkspace == null)
            {
                irMainWorkspace = SmartParts.AddNew<InquireRatesMainWorkspace>("InquireRatesMainWorkspace");
                //查询面板显示
                InquireRatesSearchPart serachPart = Items.AddNew<InquireRatesSearchPart>();
                IWorkspace searchOceanWorkspace = (IWorkspace)Workspaces[InquireRatesWorkSpaceConstants.SearchWorkspace];
                searchOceanWorkspace.Show(serachPart);
                //工具栏显示
                InquireRatesToolBar toolPart = Items.AddNew<InquireRatesToolBar>();
                IWorkspace toolBarWorkspace = (IWorkspace)Workspaces[InquireRatesWorkSpaceConstants.ToolBarWorkspace];
                toolBarWorkspace.Show(toolPart);
                //添加海运询价界面容器
                InquireOceanRatesWorkitem oceanWorkitem = WorkItems.AddNew<InquireOceanRatesWorkitem>();
                oceanWorkitem.Run();

                IWorkspace mainWorkspace = Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo { Title = "Inquire Rates" };
                mainWorkspace.Show(irMainWorkspace, smartPartInfo);

                #region 定义面板连接--稍后弄

                irMainWorkspace.PageChanging += delegate(object sender, CancelEventArgs e)
                {
                    //e.Cancel = PageChanging();      //稍后做
                };

                #endregion
            }
            else
            {
                Workspaces[ClientConstants.MainWorkspace].Activate(irMainWorkspace);
            }
        } 
        #endregion
    }

    #region 常量

    /// <summary>
    /// WorkSpace 常量
    /// </summary>
    public class InquireRatesWorkSpaceConstants
    {
        /// <summary>
        /// <c></c>
        /// </summary>
        public const string SearchWorkspace = "SearchWorkspace";
        public const string ToolBarWorkspace = "ToolbarWorkspace";
        public const string OceanRatesWorkspace = "OceanRatesWorkspace";
        public const string AirRatesWorkspace = "AirRatesWorkspace";
        public const string TruckingRatesWorkspace = "TruckingRatesWorkspace";
    }

    /// <summary>
    /// 命令常量
    /// </summary>
    public class InquireRatesCommandConstants
    {
        /// <summary>
        /// 显示查询
        /// </summary>
        public const string Command_ShowSearch = "Command_ShowSearch";
        /// <summary>
        /// 设置TabPage可用状态
        /// </summary>
        public const string Command_SetTabVisible = "Command_SetTabVisible";
        /// <summary>
        /// 子面板刷新数据
        /// </summary>
        public const string Command_SubPartRefreshData = "Command_SubPartRefreshData";
        /// <summary>
        /// 刷新数据
        /// </summary>
        public const string Command_RefreshData = "Command_RefreshData";
        /// <summary>
        /// 查询数据
        /// </summary>
        public const string Command_SearchData = "Command_SearchData";
        /// <summary>
        /// TabPage改变
        /// </summary>
        public const string Command_TabChanged = "Command_TabChanged";
        /// <summary>
        /// 新建海运询价单
        /// </summary>
        public const string Command_NewOceanRate = "Command_NewOceanRate";
        /// <summary>
        /// 新建空运询价单
        /// </summary>
        public const string Command_NewAirRate = "Command_NewAirRate";
        /// <summary>
        /// 新建拖车询价单
        /// </summary>
        public const string Command_NewTruckingRate = "Command_NewTruckingRate";
        /// <summary>
        /// 保存(商务反馈询价结果 OR 保存General Info)
        /// </summary>
        public const string Command_Save = "Command_Save";
        /// <summary>
        /// 转移询价
        /// </summary>
        public const string Command_Transit = "Command_Transit";
        /// <summary>
        /// 复制询价(子询价)
        /// </summary>
        public const string Command_Copy = "Command_Copy";
        /// <summary>
        /// 复制询价(整个询价单)
        /// </summary>
        public const string Command_ReInquire = "Command_ReInquire";
        /// <summary>
        /// 删除询价单
        /// </summary>
        public const string Command_Delete = "Command_Delete";
        /// <summary>
        /// 新建界面容器(WorkItem)
        /// </summary>
        public const string Command_AddNewWorkItem = "Command_AddNewWorkItem";
        /// <summary>
        /// 发送邮件
        /// </summary>
        public const string Command_Mail = "Command_Mail";
        /// <summary>
        /// 发送邮件到询价人(商务回复价格)
        /// </summary>
        public const string Command_SendEmailToInquireBy = "Command_SendEmailToInquireBy";
        /// <summary>
        /// 邮件通知已确认询价(发送至业务单订舱员、文件员、客服)
        /// </summary>
        public const string Command_MailBookingConfirm = "Command_MailBookingConfirm";
        /// <summary>
        /// 发送邮件到商务(操作/业务询问价格)
        /// </summary>
        public const string Command_SendEmailToRespondBy = "Command_SendEmailToRespondBy";
        /// <summary>
        /// 后台发送邮件通知
        /// </summary>
        public const string Command_BackstageSendEmailNotify = "Command_BackstageSendEmailNotify";
        /// <summary>
        /// 刷新询价的邮件面板
        /// </summary>
        public const string Command_RefreshEmailPart = "Command_RefreshEmailPart";
        /// <summary>
        /// 历史记录--查询
        /// </summary>
        public const string Command_HistoryData = "Command_HistoryData";
        /// <summary>
        /// 历史记录--替换
        /// </summary>
        public const string Command_HistoryReplace = "Command_HistoryReplace";
        /// <summary>
        /// 历史记录--复制
        /// </summary>
        public const string Command_HistoryCopy = "Command_HistoryCopy";
        /// <summary>
        /// 业务数据-确认询价
        /// </summary>
        public const string Command_ConfirmInquirePriceToShipment = "Command_ConfirmInquirePriceToShipment";
        /// <summary>
        /// 业务数据-取消确认询价
        /// </summary>
        public const string Command_Un_ConfirmInquirePriceToShipment = "Command_Un_ConfirmInquirePriceToShipment";
        /// <summary>
        /// 网格添加新记录
        /// </summary>
        public const string Command_AddNewRecord = "Command_AddNewRecord";

        #region Discussing
        public const string Command_RefreshAirDiscussingPart = "Command_RefreshAirDiscussingPart";
        public const string Command_RefreshTruckingDiscussingPart = "Command_RefreshTruckingDiscussingPart";
        public const string Command_AddDiscussing = "Command_AddDiscussing";
        public const string Command_UnReadCount = "Command_UnReadCount"; 
        #endregion
    }

    #endregion
}

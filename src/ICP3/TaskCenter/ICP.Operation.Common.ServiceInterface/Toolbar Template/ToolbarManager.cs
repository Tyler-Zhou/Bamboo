using System;
using DevExpress.XtraBars;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using System.Windows.Forms;

namespace ICP.Operation.Common.ServiceInterface
{
    /// <summary>
    /// 工具栏管理者
    /// </summary>
    public class ToolbarManager
    {
        /// <summary>
        ///动态的工具栏Element
        /// </summary>
        private const string btnDynamic = "btnDynamic";

        public static BarManager baseBarManager { get; set; }

        /// <summary>
        /// 工具栏所有项
        /// </summary>
        public static BarItems barItems
        {
            get { return baseBarManager.Items; }
        }

        public static ImageList ToolBarImages
        {
            get { return ToobarItemFactory.ImageList; }
        }

        public static WorkItem RootWorkItem
        {
            get { return ServiceClient.GetService<WorkItem>(); }
        }
        private static IOceanExportService _OeService;
        public static IOceanExportService OeService
        {
            get
            {
                if (_OeService == null)
                    _OeService = ServiceClient.GetService<IOceanExportService>();
                return _OeService;
            }
        }

        public static IBaseBusinessPart_New CurrentBaseBusinessPart
        {
            get { return RootWorkItem.State["CurrentBaseBusinessPart"] as IBaseBusinessPart_New; }
            set { RootWorkItem.State["CurrentBaseBusinessPart"] = value; }
        }

        public static OperationToolbarCommand CreateOperationToolbarCommandInfo(string id, string text, string name, string site,
                                                                                 string registerSite, MenuItemType type,
                                                                                 string imageName, bool hasPermission)
        {
            return new OperationToolbarCommand()
                {
                    Id = id,
                    Text = text,
                    Name = name,
                    Site = site,
                    RegisterSite = registerSite,
                    Type = type,
                    ImageName = imageName,
                    HasPermission = hasPermission
                };
        }

        public static OperationToolbarCommand CreateOperationToolbarCommand(Guid operationID, OperationType operationType, string templateCode)
        {
            OceanBookingInfo bookingInfo = OeService.GetOceanBookingInfo(operationID);
            if (bookingInfo == null)
                return null;
            else
                return CreateOperationToolbarCommandInfo(btnDynamic, bookingInfo.No, btnDynamic, templateCode, "",
                                                         MenuItemType.TextBox, "", false);

        }

        public static bool IsContainsToolBarElement(string name)
        {
            return barItems[name] != null;
        }

        public static void VisibleBarItem(BarManager baseBarManager, string name,
                                          BarItemVisibility visibility)
        {
            BarItem item = GetBarItem(baseBarManager, name);
            if (item != null)
                item.Visibility = visibility;
        }

        public static void VisibleBarItem(BarItems barItems, string name,
                                          BarItemVisibility visibility)
        {
            if (barItems == null) return;
            BarItem item = barItems[name] as BarItem;
            if (item != null)
                item.Visibility = visibility;
        }

        public static BarItem GetBarItem(BarManager baseBarManager, string name)
        {
            BarItem item = baseBarManager.Items[name] as BarItem;
            if (item != null)
            {
                return item;
            }
            return null;
        }

        public static BarItem GetBarItem(string name)
        {
            return baseBarManager.Items[name];
        }

        public static void RenameBarItem(string name, string caption)
        {
            BarItem item = baseBarManager.Items[name] as BarItem;
            if (item != null)
            {
                item.Caption = caption;
            }
        }

        public static BarEditItem GetBarEditItem(string name)
        {
            return baseBarManager.Items[name] as BarEditItem;
        }

        public static string GetBarEditItemEditValue(string name)
        {
            BarEditItem editItem = baseBarManager.Items[name] as BarEditItem;
            if (editItem != null)
            {
                return editItem.EditValue.ToString();
            }
            return string.Empty;
        }

        public static void RenameToolBarDynamicElement(string no)
        {
            BarItem dynamicItem = GetBarItem(btnDynamic);
            if (dynamicItem != null)
                dynamicItem.Caption = no;
        }


        public static void RenameToolBarItem(string templateCode, Guid operationID, OperationType operationType, bool isCreateDynamicBarItem)
        {
            if (templateCode.Equals(ListFormType.MailLink4NewInquireRate.ToString()) && !isCreateDynamicBarItem)
            {
                OceanBookingInfo info = OeService.GetOceanBookingInfo(operationID);
                if (info != null)
                {
                    RenameToolBarDynamicElement(info.No);
                    info = null;
                }
            }
        }




        public static void BarItemVisibility(BarManager baseBarManager, BarItemVisibility visibility)
        {
            VisibleBarItem(baseBarManager, "txtQuery", visibility);
            VisibleBarItem(baseBarManager, "btnFind", visibility);
        }
    }

}

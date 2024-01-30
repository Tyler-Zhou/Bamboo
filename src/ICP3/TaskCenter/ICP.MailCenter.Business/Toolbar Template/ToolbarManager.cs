using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraBars;
using ICP.FCM.OceanExport.ServiceInterface;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
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

        public static DevExpress.XtraBars.BarManager baseBarManager { get; set; }

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

        public static IBaseBusinessPart_New CurrentBaseBusinessPart
        {
            get { return RootWorkItem.State["CurrentBaseBusinessPart"] as IBaseBusinessPart_New; }
            set { RootWorkItem.State["CurrentBaseBusinessPart"] = value; }
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

        public static OperationToolbarCommand CreateOperationToolbarCommandInfo(string text, string name, string site,
                                                                                string registerSite, MenuItemType type,
                                                                                string imageName, bool hasPermission)
        {
            return new OperationToolbarCommand()
                {
                    Id = name,
                    Text = text,
                    Name = name,
                    Site = site,
                    RegisterSite = registerSite,
                    Type = type,
                    ImageName = imageName,
                    HasPermission = hasPermission
                };
        }

        public static bool IsContainsToolBarElement(string name)
        {
            return barItems[name] != null;
        }

        public static void VisibleBarItem(BarManager baseBarManager, string name,
                                          DevExpress.XtraBars.BarItemVisibility visibility)
        {
            BarItem item = GetBarItem(name);
            if (item != null)
                item.Visibility = visibility;
        }

        public static void VisibleBarItem(BarItems barItems, string name,
                                          DevExpress.XtraBars.BarItemVisibility visibility)
        {
            if (barItems == null) return;
            BarItem item = barItems[name] as BarItem;
            if (item != null)
                item.Visibility = visibility;
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

        public static void RenameToolBarDynamicElement(Guid operationId)
        {
            OceanBookingInfo info = OeService.GetOceanBookingInfo(operationId);
            if (info != null)
            {
                BarItem dynamicItem = GetBarItem(btnDynamic);
                if (dynamicItem != null)
                    dynamicItem.Caption = info.No;
            }
        }


    }
}

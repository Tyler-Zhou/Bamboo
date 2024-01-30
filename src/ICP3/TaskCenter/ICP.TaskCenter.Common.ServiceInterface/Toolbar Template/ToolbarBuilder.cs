using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.MailCenter.Business.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using DevExpress.XtraBars;

namespace ICP.MailCenter.Business.ServiceInterface
{
    public class ToolbarBuilder
    {
        OperationToolbarCommand cmd;
        public ToolbarBuilder(OperationToolbarCommand mapping)
        {
            this.cmd = mapping;
        }

        /// <summary>
        /// 当前的业务处理面板
        /// </summary>
        public BaseBusinessPart CurrentBaseBusinessPart { get; set; }
        static int i = 0;

        public DevExpress.XtraBars.BarItem BuildIn(WorkItem workitem,BarManager manager)
        {
            DevExpress.XtraBars.BarItem baritem = ToobarItemFactory.GetBarItem(cmd,manager);
            baritem.Name = this.cmd.Name;
            if (!string.IsNullOrEmpty(cmd.Name) && cmd.Type != MenuItemType.TextBox && cmd.Type != MenuItemType.ComboBox)
            {

                baritem.ItemClick += (sender, e) =>
                {
                    workitem.RootWorkItem.State["CurrentBaseBusinessPart"] = CurrentBaseBusinessPart;
                    workitem.Commands[cmd.Name].Execute();
                };
               // workitem.Commands[cmd.Name].AddInvoker(baritem, "ItemClick");
            }
            else if (!string.IsNullOrEmpty(cmd.Name) && cmd.Type == MenuItemType.ComboBox)
            {
               (baritem as BarEditItem).EditValueChanged += (sender, e) =>
                {
                    workitem.RootWorkItem.State["CurrentBaseBusinessPart"] = CurrentBaseBusinessPart;
                    workitem.Commands[cmd.Name].Execute(); 
                };
              // workitem.Commands[cmd.Name].AddInvoker((baritem as BarEditItem).Edit, "EditValueChanged");
                
            }
            baritem.Tag = cmd;
            if (cmd.Type != MenuItemType.TextBox)
            {
                baritem.Caption = cmd.Text;
            }
            baritem.Id = i;
            baritem.Name = cmd.Id;
            baritem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;

            i++;
           
            workitem.UIExtensionSites[cmd.Site].Add(baritem);
            string registerSiteName = cmd.RegisterSite;
            if ( !string.IsNullOrEmpty(registerSiteName) && !workitem.UIExtensionSites.Contains(registerSiteName))
                workitem.UIExtensionSites.RegisterSite(registerSiteName, baritem);
            
            //baritem.Enabled = cmd.HasPermission;
            return baritem;

        }


    }
}

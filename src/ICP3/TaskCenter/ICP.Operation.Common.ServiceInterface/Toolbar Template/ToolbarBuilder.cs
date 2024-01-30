using Microsoft.Practices.CompositeUI;
using DevExpress.XtraBars;

namespace ICP.Operation.Common.ServiceInterface
{
    public class ToolbarBuilder
    {
        OperationToolbarCommand cmd;
        public ToolbarBuilder(OperationToolbarCommand mapping)
        {
            cmd = mapping;
        }

        /// <summary>
        /// 当前的业务处理面板
        /// </summary>
        public IBaseBusinessPart_New CurrentBaseBusinessPart { get; set; }
        public string RegisterSiteName { get; set; }

        static int i = 0;
        public BarItem BuildIn(WorkItem workitem, BarManager manager)
        {
            BarItem baritem = ToobarItemFactory.GetBarItem(cmd, manager);
            baritem.Name = cmd.Name;
            if (!string.IsNullOrEmpty(cmd.Name) && cmd.Type != MenuItemType.TextBox && cmd.Type != MenuItemType.ComboBox)
            {

                baritem.ItemClick += (sender, e) =>
                    {
                        var checkItem = e.Item as BarCheckItem;
                        if (checkItem != null)
                        {
                            workitem.RootWorkItem.State["SOSetting"] = (e.Item as BarCheckItem).Checked;
                        }

                        workitem.RootWorkItem.State["CurrentBaseBusinessPart"] = CurrentBaseBusinessPart;
                        workitem.Commands[cmd.Name].Execute();

                    };
                // workitem.Commands[cmd.Name].AddInvoker(baritem, "ItemClick");
            }
            else if (!string.IsNullOrEmpty(cmd.Name) && cmd.Type == MenuItemType.ComboBox)
            {
                (baritem as BarEditItem).EditValueChanged += (sender, e) =>
                    {
                        workitem.RootWorkItem.State["BarEditItemChangedValue"] = (sender as BarEditItem).EditValue;
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
            if (cmd.Width > 0)
            {
                baritem.Width = cmd.Width;
            }
            baritem.Name = cmd.Id;
            baritem.Hint = cmd.Tooltip;
            baritem.PaintStyle = BarItemPaintStyle.CaptionGlyph;
            baritem.Enabled = cmd.Enabled;
            baritem.Visibility = cmd.Visible ? BarItemVisibility.Always : BarItemVisibility.Never;
            i++;


            workitem.UIExtensionSites[cmd.Site].Add(baritem);

            string registerSiteName = cmd.RegisterSite;
            if (!string.IsNullOrEmpty(registerSiteName) && !workitem.UIExtensionSites.Contains(registerSiteName))
            {
                RegisterSiteName = registerSiteName;
                CurrentBaseBusinessPart.RegisterSiteNames.Add(registerSiteName);
                workitem.UIExtensionSites.RegisterSite(registerSiteName, baritem);
            }

            //baritem.Enabled = cmd.HasPermission;
            return baritem;

        }
        //public DevExpress.XtraBars.BarItem BuildIn(WorkItem workitem,BarManager manager)
        //{
        //    DevExpress.XtraBars.BarItem baritem = ToobarItemFactory.GetBarItem(cmd,manager);
        //    baritem.Name = this.cmd.Name;
        //    baritem.Hint = this.cmd.Tooltip;
        //    if (!string.IsNullOrEmpty(cmd.Name) && cmd.Type != MenuItemType.TextBox && cmd.Type != MenuItemType.ComboBox)
        //    {
        //        workitem.Commands[cmd.Name].AddInvoker(baritem, "ItemClick");
        //    }
        //    else if (!string.IsNullOrEmpty(cmd.Name) && cmd.Type == MenuItemType.ComboBox)
        //    {
        //       workitem.Commands[cmd.Name].AddInvoker((baritem as BarEditItem).Edit, "EditValueChanged");

        //    }
        //    baritem.Tag = cmd;
        //    if (cmd.Type != MenuItemType.TextBox)
        //    {
        //        baritem.Caption = cmd.Text;
        //    }
        //    baritem.Id = i;
        //    baritem.Name = cmd.Id;
        //    baritem.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;

        //    i++;

        //    workitem.UIExtensionSites[cmd.Site].Add(baritem);
        //    string registerSiteName = cmd.RegisterSite;
        //    if (string.IsNullOrEmpty(cmd.Name) && !string.IsNullOrEmpty(registerSiteName) && !workitem.UIExtensionSites.Contains(registerSiteName))
        //        workitem.UIExtensionSites.RegisterSite(registerSiteName, baritem);

        //    //baritem.Enabled = cmd.HasPermission;
        //    return baritem;

        //}


    }
}

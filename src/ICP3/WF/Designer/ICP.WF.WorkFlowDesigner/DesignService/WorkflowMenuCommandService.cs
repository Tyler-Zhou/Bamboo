using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;

namespace ICP.WF.WorkFlowDesigner
{

    /// <summary>
    ///  提供的方法用于管理设计模式下可用的全局设计器谓词和菜单命令，以及显示某些类型的快捷菜单。
    /// </summary>
    internal sealed class WorkflowMenuCommandService : MenuCommandService
    {
        #region 构造函数

        public WorkflowMenuCommandService(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }
        #endregion

        #region 重载方法
        /// <summary>
        /// 显示上下文菜单
        /// </summary>
        /// <param name="menuID">菜单ID</param>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        public override void ShowContextMenu(CommandID menuID, int x, int y)
        {
            if (menuID == WorkflowMenuCommands.SelectionMenu)
            {
                ContextMenu contextMenu = new ContextMenu();

                foreach (DesignerVerb verb in Verbs)
                {
                    MenuItem menuItem = new MenuItem(verb.Text, new EventHandler(OnMenuClicked));
                    menuItem.Tag = verb;
                    contextMenu.MenuItems.Add(menuItem);
                }

                MenuItem[] items = GetSelectionMenuItems();
                if (items.Length > 0)
                {
                    contextMenu.MenuItems.Add(new MenuItem("-"));
                    foreach (MenuItem item in items)
                    {
                        contextMenu.MenuItems.Add(item);
                    }
                }

                WorkflowView workflowView = GetService(typeof(WorkflowView)) as WorkflowView;
                if (workflowView != null)
                {
                    contextMenu.Show(workflowView, workflowView.PointToClient(new Point(x, y)));
                }
            }
        }
        #endregion


        #region 本地方法
        
        private void OnMenuClicked(object sender, EventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            if (menuItem != null && menuItem.Tag is MenuCommand)
            {
                MenuCommand command = menuItem.Tag as MenuCommand;
                command.Invoke();
            }
        }

        private MenuItem[] GetSelectionMenuItems()
        {
            List<MenuItem> menuItems = new List<MenuItem>();

            bool addMenuItems = true;
            ISelectionService selectionService = GetService(typeof(ISelectionService)) as ISelectionService;
            if (selectionService != null)
            {
                foreach (object obj in selectionService.GetSelectedComponents())
                {
                    if (!(obj is Activity))
                    {
                        addMenuItems = false;
                        break;
                    }
                }
            }

            if (addMenuItems)
            {
                Dictionary<CommandID, string> selectionCommands = new Dictionary<CommandID, string>();
                selectionCommands.Add(WorkflowMenuCommands.Cut, "Cut");
                selectionCommands.Add(WorkflowMenuCommands.Copy, "Copy");
                selectionCommands.Add(WorkflowMenuCommands.Paste, "Paste");
                selectionCommands.Add(WorkflowMenuCommands.Delete, "Delete");


                foreach (CommandID id in selectionCommands.Keys)
                {
                    MenuCommand command = FindCommand(id);
                    if (command != null)
                    {
                        MenuItem menuItem = new MenuItem(selectionCommands[id], new EventHandler(OnMenuClicked));
                        menuItem.Tag = command;
                        menuItems.Add(menuItem);
                    }
                }
            }

            return menuItems.ToArray();
        }

        #endregion

    }
}
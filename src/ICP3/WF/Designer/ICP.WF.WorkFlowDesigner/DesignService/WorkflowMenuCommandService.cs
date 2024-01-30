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
    ///  �ṩ�ķ������ڹ������ģʽ�¿��õ�ȫ�������ν�ʺͲ˵�����Լ���ʾĳЩ���͵Ŀ�ݲ˵���
    /// </summary>
    internal sealed class WorkflowMenuCommandService : MenuCommandService
    {
        #region ���캯��

        public WorkflowMenuCommandService(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }
        #endregion

        #region ���ط���
        /// <summary>
        /// ��ʾ�����Ĳ˵�
        /// </summary>
        /// <param name="menuID">�˵�ID</param>
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


        #region ���ط���
        
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
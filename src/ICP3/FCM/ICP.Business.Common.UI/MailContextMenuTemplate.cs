#region Comment

/*
 * 
 * FileName:    MailContextMenuTemplate.cs
 * CreatedOn:   2014/9/17 16:48:54
 * CreatedBy:   taylor 
 * 
 * 
 * Description：
 *      ->
 * History：
 *      ->
 * 
 * 
 * 
 * 
 */

#endregion

using System.Linq;
using ICP.Business.Common.ServiceInterface;
using ICP.Framework.CommonLibrary;
using ICP.Framework.CommonLibrary.Client;
using ICP.Operation.Common.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ICP.Business.Common.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class MailContextMenuTemplate
    {
        #region 全局变量，服务
        /// <summary>
        /// 
        /// </summary>
        public WorkItem RootWorkItem
        {
            get { return ServiceClient.GetClientService<WorkItem>(); }
        }


        #endregion

        /// <summary>
        /// 生成ToolStripMenuItem集合
        /// </summary>
        /// <returns></returns>
        public List<ToolStripMenuItem> GetMenuItems()
        {
            List<ToolStripMenuItem> items = new List<ToolStripMenuItem>();
            if (RootWorkItem.State["SendEmailTempCode"] == null)
                ServiceClient.GetService<IICPCommonOperationService>().GetTemplateCodeFromTaskCenter();
            List<string> templateCodes = RootWorkItem.State["SendEmailTempCode"] as List<string>;
            if (templateCodes != null)
            {
                List<OperationToolbarCommand> commandList = new List<OperationToolbarCommand>();
                foreach (var templateCode in templateCodes)
                {
                    commandList.AddRange(ToolbarTemplateLoader.Current[templateCode]);
                }
                if (commandList.Count <= 0)
                    return items;
                items.AddRange(commandList.Select(GetToolStripMenuItem).Where(tool => tool != null).Distinct(new CustomPropertyComparer<ToolStripMenuItem>("Name")));
            }
            return items;
        }
        /// <summary>
        /// 根据XML构造对应的菜单项
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        private ToolStripMenuItem GetToolStripMenuItem(OperationToolbarCommand cmd)
        {
            if (string.IsNullOrEmpty(cmd.ClickOperation) || cmd.ClickOperation != "SendEmail")
                return null;
            ToolStripMenuItem tool = new ToolStripMenuItem();

            tool.Name = cmd.Name;
            if (!string.IsNullOrEmpty(cmd.Name))
            {
                tool.Click += (sender, e) =>RootWorkItem.Commands[cmd.Name].Execute();
            }
            tool.Tag = cmd;
            if (cmd.Type != MenuItemType.TextBox)
            {
                tool.Text = cmd.Text;
            }
            tool.Enabled = cmd.Enabled;
            tool.Visible = cmd.Visible;
            return tool;
        }
    }
}

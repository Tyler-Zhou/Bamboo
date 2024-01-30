using System;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Client;
using ICP.MailCenter.ServiceInterface;

namespace ICP.MailCenter.UI
{
    public static class MenuService
    {
        public static ContextMenuStrip CreateContextMenu(EventHandler menuItemClickHandler)
        {

            ContextMenuStrip contextMenu = new ContextMenuStrip();
            InitContextMenu(contextMenu.Items, menuItemClickHandler);
            return contextMenu;
        }
        /// <summary>
        ///构造联系人邮件菜单
        /// </summary>
        /// <param name="entryID"></param>
        private static void InitContextMenu(ToolStripItemCollection items, EventHandler menuItemClickHandler)
        {
            bool isEnglish = LocalData.IsEnglish;
            ToolStripMenuItem mailItem = new ToolStripMenuItem();
            mailItem.Text = isEnglish ? "New Mail" : "新增邮件";
            mailItem.Tag = ContactType.NewMail;
            mailItem.Click += menuItemClickHandler;
            items.Add(mailItem);

            mailItem = new ToolStripMenuItem();
            mailItem.Text = isEnglish ? "Add to Contact" : "添加到联系人";
            mailItem.Tag = ContactType.AddToContact;
            mailItem.Click += menuItemClickHandler;
            items.Add(mailItem);

            mailItem = new ToolStripMenuItem();
            mailItem.Text = LocalData.IsEnglish ? "Copy Contact" : "复制联系人";
            mailItem.Tag = ContactType.CopyContact;
            mailItem.Click += menuItemClickHandler;
            items.Add(mailItem);

            mailItem = new ToolStripMenuItem();
            mailItem.Text = isEnglish ? "History Sending by the Contact" : "以联系人作为发件人历史记录";
            mailItem.Tag = ContactType.SendingByContact;
            mailItem.Click += menuItemClickHandler;
            items.Add(mailItem);

            mailItem = new ToolStripMenuItem();
            mailItem.Text = isEnglish ? "History Receiving by the Contact" : "以联系人作为接收人历史记录";
            mailItem.Tag = ContactType.ReceivingByContact;
            mailItem.Click += menuItemClickHandler;
            items.Add(mailItem);

            mailItem = new ToolStripMenuItem();
            mailItem.Text = isEnglish ? "History Sending/Receiving by the Contact" : "以联系人作为接收人和发件人历史记录";
            mailItem.Tag = ContactType.SendingReceivingByContact;
            mailItem.Click += menuItemClickHandler;
            items.Add(mailItem);

        }
    }
}

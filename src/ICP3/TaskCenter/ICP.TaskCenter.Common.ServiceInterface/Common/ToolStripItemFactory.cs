using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ICP.MailCenter.Business.ServiceInterface;

namespace ICP.MailCenter.Business.ServiceInterface
{ 
    /// <summary>
    /// 
    /// </summary>
  public  class ToolStripItemFactory
    {
        private static ImageList imageList;
        public static ImageList ImageList
        {
            get {
                if (imageList == null)
                {
                    CreateImageList(GetImageFilePath());
                }
                return imageList;
            }
        }
        private static string GetImageFilePath()
        {
            string root = AppDomain.CurrentDomain.BaseDirectory;
            string relativePath = Path.Combine("Images", "ContextMenu");
            return Path.Combine(root, relativePath);
        }

        private static void CreateImageList(string imageFilePath)
        {
            
                imageList = UIHelper.GetImageList(imageFilePath);
           

        }
        public static ToolStripItem GetToolStripItem(ContextMenuItemInfo itemInfo)
        {
            ToolStripItem item = null;
            switch (itemInfo.Type)
            { 
                case ContexuMenuItemType.MenuItem:
                    item = CreateMenuItem(itemInfo);
                    break;
                case ContexuMenuItemType.Separator:
                    item = CreateSeparator(itemInfo);
                    break;

            }
            return item;
        }

        private static ToolStripItem CreateSeparator(ContextMenuItemInfo itemInfo)
        {
            ToolStripSeparator separator = new ToolStripSeparator();
            return separator;
        }

        private static ToolStripItem CreateMenuItem(ContextMenuItemInfo itemInfo)
        {
            ToolStripMenuItem item = new ToolStripMenuItem();
            item.Text = itemInfo.Text;
            item.TextImageRelation = TextImageRelation.ImageBeforeText;
            item.ToolTipText = itemInfo.Text;
            if(!string.IsNullOrEmpty(itemInfo.ImageName))
            {
                item.Image = ImageList.Images[itemInfo.ImageName];
            }
            return item;
        }

     

    }
}

using DevExpress.Utils.Menu;
using System;
using System.IO;
using System.Windows.Forms;

namespace ICP.Operation.Common.ServiceInterface
{
    /// <summary>
    /// 
    /// </summary>
    public class MenuItemFactory
    {
        /// <summary>
        /// 
        /// </summary>
        private static ImageList imageList;
        /// <summary>
        /// 
        /// </summary>
        public static ImageList ImageList
        {
            get
            {
                if (imageList == null)
                {
                    CreateImageList(GetImageFilePath());
                }
                return imageList;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static string GetImageFilePath()
        {
            string root = AppDomain.CurrentDomain.BaseDirectory;
            string relativePath = Path.Combine("Images", "ContextMenu");
            return Path.Combine(root, relativePath);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="imageFilePath"></param>
        private static void CreateImageList(string imageFilePath)
        {
            imageList = UIHelper.GetImageList(imageFilePath);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static DXMenuItem GetMenuItem(ContextMenuItemInfo itemInfo, MenuItemTag tag)
        {
            DXMenuItem item = null;
            switch (itemInfo.Type)
            {
                case ContextMenuItemType.MenuItem:
                    item = CreateMenuItem(itemInfo, tag);
                    break;
                case ContextMenuItemType.SubMenuItem:
                    item = CreateSubMenuItem(itemInfo, tag);
                    break;

            }
            return item;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        private static DXSubMenuItem CreateSubMenuItem(ContextMenuItemInfo itemInfo, MenuItemTag tag)
        {
            DXSubMenuItem item = new DXSubMenuItem();
            InitItem(item, itemInfo, tag);
            return item;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="itemInfo"></param>
        /// <param name="tag"></param>
        private static void InitItem(DXMenuItem item, ContextMenuItemInfo itemInfo, MenuItemTag tag)
        {

            item.Caption = itemInfo.Text;
            item.Enabled = true;
            item.Visible = true;
            item.BeginGroup = itemInfo.BeginGroup;
            item.Tag = tag;
            if (!string.IsNullOrEmpty(itemInfo.ImageName))
            {
                item.Image = ImageList.Images[itemInfo.ImageName];
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        private static DXMenuItem CreateMenuItem(ContextMenuItemInfo itemInfo, MenuItemTag tag)
        {
            DXMenuItem item = new DXMenuItem();
            InitItem(item, itemInfo, tag);
            return item;
        }



    }
    /// <summary>
    /// 右键菜单项Tag信息类
    /// </summary>
    public struct MenuItemTag
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name;
        /// <summary>
        /// 
        /// </summary>
        public object Tag;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="tag"></param>
        public MenuItemTag(string name, object tag)
        {
            Name = name;
            Tag = tag;
        }
    }
}

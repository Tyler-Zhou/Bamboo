using System;
using System.Windows.Forms;
using System.IO;
using DevExpress.Utils.Menu;

namespace ICP.Operation.Common.ServiceInterface
{ 
    /// <summary>
    /// 
    /// </summary>
  public  class MenuItemFactory
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
        public static DXMenuItem GetMenuItem(ContextMenuItemInfo itemInfo, MenuItemTag tag)
        {
            DXMenuItem item = null;
            switch (itemInfo.Type)
            { 
                case ContextMenuItemType.MenuItem:
                    item = CreateMenuItem(itemInfo,tag);
                    break;
                case ContextMenuItemType.SubMenuItem:
                    item = CreateSubMenuItem(itemInfo, tag);
                    break;

            }
            return item;
        }

        private static DXSubMenuItem CreateSubMenuItem(ContextMenuItemInfo itemInfo, MenuItemTag tag)
        {
            DXSubMenuItem item = new DXSubMenuItem();
            InitItem(item, itemInfo, tag);
            return item;
            
        }
      private static void InitItem(DXMenuItem item,ContextMenuItemInfo itemInfo, MenuItemTag tag)
      {
          
            item.Caption = itemInfo.Text;
            item.Enabled = true;
            item.Visible = true;
            item.BeginGroup = itemInfo.BeginGroup;
            item.Tag = tag;
            if(!string.IsNullOrEmpty(itemInfo.ImageName))
            {
                item.Image = ImageList.Images[itemInfo.ImageName];
            }
      }

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
      public string Name;
      public object Tag;
      public MenuItemTag(string name, object tag)
      {
          this.Name = name;
          this.Tag = tag;
      }
  }
}

using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Menu;
using Microsoft.Practices.CompositeUI.UIElements;
using System;

namespace ICP.Operation.Common.ServiceInterface
{
    /// <summary>
    /// 
    /// </summary>
    public class GridMenuUIAdapterFactory : IUIElementAdapterFactory
    {
       /// <summary>
       /// 
       /// </summary>
       /// <param name="uiElement"></param>
       /// <returns></returns>
        public IUIElementAdapter GetAdapter(object uiElement)
        {
          if(uiElement is GridViewMenu)
          {
            return new MenuItemCollectionUIAdapter(((GridViewMenu)uiElement).Items);
          }
          else if (uiElement is DXMenuItem)
				return new MenuItemOwnerCollectionUIAdapter((DXMenuItem)uiElement);
          throw new ArgumentException("uiElement");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uiElement"></param>
        /// <returns></returns>
        public bool Supports(object uiElement)
        {
           return (uiElement is GridViewMenu) || (uiElement is DXMenuItem);
        }
    }
}

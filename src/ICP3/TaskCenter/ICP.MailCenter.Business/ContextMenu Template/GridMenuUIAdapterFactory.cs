using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.CompositeUI.UIElements;
using DevExpress.XtraGrid.Menu;
using DevExpress.Utils.Menu;

namespace ICP.Operation.Common.ServiceInterface
{
    public class GridMenuUIAdapterFactory : IUIElementAdapterFactory
    {
       
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

        public bool Supports(object uiElement)
        {
           return (uiElement is GridViewMenu) || (uiElement is DXMenuItem);
        }

        
    }
}

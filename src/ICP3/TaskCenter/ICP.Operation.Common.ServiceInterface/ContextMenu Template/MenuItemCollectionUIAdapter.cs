using DevExpress.Utils.Menu;
using Microsoft.Practices.CompositeUI.UIElements;
using Microsoft.Practices.CompositeUI.Utility;
using System;

namespace ICP.Operation.Common.ServiceInterface
{
    /// <summary>
    /// 
    /// </summary>
    public class MenuItemCollectionUIAdapter : UIElementAdapter<DXMenuItem>
    {
        /// <summary>
        /// 
        /// </summary>
        private DXMenuItemCollection collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItemCollectionUIAdapter"/> class.
        /// </summary>
        /// <param name="collection"></param>
        public MenuItemCollectionUIAdapter(DXMenuItemCollection collection)
        {
            Guard.ArgumentNotNull(collection, "collection");
            this.collection = collection;
        }

        /// <summary>
        /// See <see cref="UIElementAdapter{TUIElement}.Add(TUIElement)"/> for more information.
        /// </summary>
        protected override DXMenuItem Add(DXMenuItem uiElement)
        {
            if (collection == null)
                throw new InvalidOperationException();

            collection.Insert(GetInsertingIndex(uiElement), uiElement);
            return uiElement;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="uiElement"></param>
        /// <returns></returns>
        public override DXMenuItem Insert(DXMenuItem uiElement, int index)
        {
            if (collection == null)
                throw new InvalidOperationException();

            collection.Insert(index, uiElement);
            return uiElement;
        }


        /// <summary>
        /// See <see cref="UIElementAdapter{TUIElement}.Remove(TUIElement)"/> for more information.
        /// </summary>
        protected override void Remove(DXMenuItem uiElement)
        {
            if (uiElement.Collection != null)
                uiElement.Collection.Remove(uiElement);
        }

        /// <summary>
        /// When overridden in a derived class, returns the correct index for the item being added. By default,
        /// it will return the length of the collection.
        /// </summary>
        /// <param name="uiElement"></param>
        /// <returns></returns>
        protected virtual int GetInsertingIndex(object uiElement)
        {
            return collection.Count;
        }

        /// <summary>
        /// Returns the internal collection mananged by the <see cref="MenuItemCollectionUIAdapter"/>
        /// </summary>
        protected DXMenuItemCollection InternalCollection
        {
            get { return collection; }
            set { collection = value; }
        }
    }
}

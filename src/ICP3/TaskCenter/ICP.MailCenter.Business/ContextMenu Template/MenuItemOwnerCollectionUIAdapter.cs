using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Utils.Menu;

namespace ICP.Operation.Common.ServiceInterface
{
    public class MenuItemOwnerCollectionUIAdapter : MenuItemCollectionUIAdapter
    {
        DXMenuItem item;

		/// <summary>
        /// Initializes a new instance of the <see cref="MenuItemOwnerCollectionUIAdapter"/> using the
		/// specified item.
		/// </summary>
		/// <param name="item"></param>
        public MenuItemOwnerCollectionUIAdapter(DXMenuItem item)
			: base(item.Collection)
		{
			this.item = item;
            item.Collection.CollectionChanged += new System.ComponentModel.CollectionChangeEventHandler(Collection_CollectionChanged);
		}

        void Collection_CollectionChanged(object sender, System.ComponentModel.CollectionChangeEventArgs e)
        {
            if (item.Collection == null)
                InternalCollection = null;
            else
                InternalCollection = item.Collection;
        }
        /// <summary>
        /// 覆盖基类插入方法
        /// </summary>
        /// <param name="uiElement"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public override DXMenuItem Insert(DXMenuItem uiElement, int index)
        {
            //return base.Insert(uiElement, index);
            if (this.item is DXSubMenuItem)
            {
                ((DXSubMenuItem)this.item).Items.Insert(index, uiElement);

            }
            else
                base.Insert(uiElement, index);
            return uiElement;
        }

		/// <summary>
        /// Returns the index immediately after the <see cref="DXMenuItem"/> that
		/// was provided to the constructor.
		/// </summary>
		/// <param name="uiElement"></param>
		/// <returns></returns>
		protected override int GetInsertingIndex(object uiElement)
		{
            for (int i = 0; i < InternalCollection.Count; i++)
            {
                if (InternalCollection[i] == uiElement)
                    return i + 1;
            }
		    throw new InvalidOperationException();

			
		}

    }
}

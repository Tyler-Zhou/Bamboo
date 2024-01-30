
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;

namespace ICP.WF.WorkFlowDesigner
{
    /// <summary>
    /// 实现一个接口,用于为属性浏览器中显示的组件的属性管理图像、工具提示和事件处理程序
    /// </summary>
    public class PropertyValueUIService : IPropertyValueUIService
    {
        #region 本地变量

        private PropertyValueUIHandler _handler;
        private EventHandler _notifyHandler;
        private ArrayList _itemList;

        #endregion

        #region 构造函数

        public PropertyValueUIService()
        {
        }

        #endregion

        #region IPropertyValueUIService接口成员实现

        /// <summary>
        /// 当修改 PropertyValueUIItem 对象的列表时发生。
        /// </summary>
        event EventHandler IPropertyValueUIService.PropertyUIValueItemsChanged
        {
            add
            {
                _notifyHandler += value;
            }
            remove
            {
                _notifyHandler -= value;
            }
        }

        /// <summary>
        /// 将指定的 PropertyValueUIHandler 添加到此服务。
        /// </summary>
        /// <param name="newHandler"></param>
        void IPropertyValueUIService.AddPropertyValueUIHandler(PropertyValueUIHandler newHandler)
        {
            if (newHandler == null)
            {
                throw new ArgumentNullException("newHandler");
            }

            _handler = (PropertyValueUIHandler)Delegate.Combine(_handler, newHandler);
        }

       /// <summary>
        /// 获取与指定的上下文和属性描述符特征匹配的 PropertyValueUIItem 对象。
       /// </summary>
       /// <param name="context"></param>
       /// <param name="propDesc"></param>
       /// <returns></returns>
        PropertyValueUIItem[] IPropertyValueUIService.GetPropertyUIValueItems(ITypeDescriptorContext context, PropertyDescriptor propDesc)
        {

            if (propDesc == null)
            {
                throw new ArgumentNullException("propDesc");
            }

            if (_handler == null)
            {
                return new PropertyValueUIItem[0];
            }

            lock (this)
            {
                if (_itemList == null)
                {
                    _itemList = new ArrayList();
                }

                _handler(context, propDesc, _itemList);

                int nItems = _itemList.Count;

                if (nItems > 0)
                {
                    PropertyValueUIItem[] items = new PropertyValueUIItem[nItems];
                    _itemList.CopyTo(items, 0);
                    _itemList.Clear();
                    return items;
                }
            }

            return null;
        }

        /// <summary>
        /// 通知 IPropertyValueUIService 实现：PropertyValueUIItem 对象的全局列表已修改。
        /// </summary>
        void IPropertyValueUIService.NotifyPropertyValueUIItemsChanged()
        {
            if (_notifyHandler != null)
            {
                _notifyHandler(this, EventArgs.Empty);
            }
        }

        /// <summary>
        ///从属性值 UI 服务中移除指定的 PropertyValueUIHandler。 
        /// </summary>
        /// <param name="newHandler"></param>
        void IPropertyValueUIService.RemovePropertyValueUIHandler(PropertyValueUIHandler newHandler)
        {
            if (newHandler == null)
            {
                throw new ArgumentNullException("newHandler");
            }

            _handler = (PropertyValueUIHandler)Delegate.Remove(_handler, newHandler);
        }

        #endregion
    }
   
}

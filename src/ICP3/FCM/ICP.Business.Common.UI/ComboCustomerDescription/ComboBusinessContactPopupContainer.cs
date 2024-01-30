using DevExpress.XtraEditors;
using ICP.Framework.ClientComponents.Controls;
using System;
using System.Drawing;

namespace ICP.Business.Common.UI
{
    internal class ComboBusinessContactPopupContainer : BusinessContactPopupContainerEdit
    {
        protected override Rectangle ConstrainFormBounds(Rectangle r)
        {
            switch (PopupFormPosition)
            {
                case PopupFormPosition.Left:
                    r.Location = new Point(r.Location.X - Parent.Width+2*Width, r.Location.Y);
                    break;
                case PopupFormPosition.Top:
                    r.Location = new Point(r.Location.X-Parent.Width+Width+3, r.Location.Y);
                    break;
                case PopupFormPosition.Right:
                    r.Location = new Point(r.Location.X , r.Location.Y );
                    break;
                default:
                    r.Location = new Point(r.Location.X - Parent.Width + Width + 3, r.Location.Y);
                    break;
            }
            return base.ConstrainFormBounds(r);
        }

        /// <summary>
        /// 失去焦点
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLostFocus(EventArgs e)
        {
            BaseOnLostFocus(e);
        }

        /// <summary>
        /// 关闭组件
        /// </summary>
        /// <param name="closeMode"></param>
        protected override void ClosePopup(PopupCloseMode closeMode)
        {
            BaseClosePopup(closeMode);
        }
    }
}

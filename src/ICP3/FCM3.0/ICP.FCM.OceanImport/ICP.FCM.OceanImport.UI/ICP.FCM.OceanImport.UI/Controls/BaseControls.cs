using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ComponentModel;

namespace ICP.FCM.OceanImport.UI
{
    /// <summary>
    /// 把一些控件的Enter键换成Tab键
    /// </summary>
    [ToolboxItem(false)]
    public class UCtrl:DevExpress.XtraEditors.XtraUserControl
    {
        public UCtrl() 
        {
            if (DesignMode == false)
            {
                _UnProcessControls.Add(typeof(ICP.Framework.ClientComponents.Controls.LWGridControl));
                _UnProcessControls.Add(typeof(ICP.Framework.ClientComponents.Controls.CustomerPopupContainerEdit));
                _UnProcessControls.Add(typeof(ICP.Framework.ClientComponents.Controls.ComboCustomerDescriptionControl));
                _UnProcessControls.Add(typeof(DevExpress.XtraEditors.ButtonEdit));
                _UnProcessControls.Add(typeof(DevExpress.XtraGrid.Views.Grid.GridView));
                _UnProcessControls.Add(typeof(DevExpress.XtraEditors.PopupContainerEdit));
            }       
        }

        List<Type> _UnProcessControls = new List<Type>();
        /// <summary>
        /// 不处理的控件集合
        /// </summary>
        protected List<Type> UnProcessControls
        {
            get { return _UnProcessControls; }
            set { _UnProcessControls = value; }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter && IsProcessEnter() == true)
            {
                SendKeys.Send("{Tab}");

                return true;
            }

            return base.ProcessDialogKey(keyData);
        }
            
        /// <summary>
        /// 判断是否要对Enter键进行处理
        /// </summary>
        /// <returns></returns>
        protected virtual bool IsProcessEnter()
        {
            Type controlType =ActiveControl.GetType();

            if (_UnProcessControls.Contains(controlType))
            {
                return false;
            }
            else
            {
                if (ActiveControl.Parent != null && ActiveControl.Parent.GetHashCode() != this.GetHashCode())
                {
                    Type parentControlType = ActiveControl.Parent.GetType();
                    if (_UnProcessControls.Contains(parentControlType))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}

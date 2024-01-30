using DevExpress.XtraEditors;

namespace ICP.FCM.AirImport.UI
{
    public class UnClosePopupContainerEdit : PopupContainerEdit
    {
        /// <summary>
        /// 已禁止DEV自带的关闭,请使用ClosePopupControl()方法
        /// </summary>
        protected override void DoClosePopup(PopupCloseMode closeMode)
        {
            //DoNothing
        }
        /// <summary>
        /// 已禁止DEV自带的关闭,请使用ClosePopupControl()方法
        /// </summary>
        public override void ClosePopup()
        {
            //base.DoClosePopup(PopupCloseMode.Normal);
        }

        /// <summary>
        /// 关闭弹出框
        /// </summary>
        public void ClosePopupControl()
        {
            base.DoClosePopup(PopupCloseMode.Normal);
        }
    }
}

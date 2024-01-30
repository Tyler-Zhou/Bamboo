#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/3/13 星期二 14:41:02
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using ICP.FCM.OtherBusiness.ServiceInterface;
using ICP.FCM.OtherBusiness.UI.Common;
using System.ComponentModel;

namespace ICP.FCM.OtherBusiness.UI.Order
{
    /// <summary>
    /// 虚拟页面(其他业务--订单编辑）
    /// </summary>
    [ToolboxItem(false)]
    public class OBOrderBaseEditPart : OBEditPart
    {
        /// <summary>
        /// 是否订单编辑页面
        /// </summary>
        public override bool IsOrderEditPart
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// 添加业务类型
        /// </summary>
        public override AddType AddBusinessType
        {
            get { return AddType.OtherBusinessOrder; }
        }
    }
}

#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/3/15 星期四 17:35:33
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

namespace ICP.FCM.OtherBusiness.UI.Business
{
    /// <summary>
    /// 
    /// </summary>
    [ToolboxItem(false)]
    public class OBBaseEditPart : OBEditPart
    {
        /// <summary>
        /// 是否订单编辑页面
        /// </summary>
        public override bool IsOrderEditPart
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 添加业务类型
        /// </summary>
        public override AddType AddBusinessType
        {
            get { return AddType.OtherBusiness; }
        }
    }
}

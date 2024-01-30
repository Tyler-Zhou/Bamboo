#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/7/11 星期三 16:30:50
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using ICP.FCM.Common.ServiceInterface.DataObjects;

namespace ICP.FCM.Common.ServiceInterface
{
    /// <summary>
    /// 枚举扩展方法
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// 海进订单状态 >>> 订单状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static OrderState ToOState(this OIOrderState input)
        {
            OrderState orderState = OrderState.Unknown;
            switch (input)
            {
                case OIOrderState.NewOrder:
                    orderState = OrderState.NewOrder;
                    break;
                case OIOrderState.Rejected:
                    orderState = OrderState.Rejected;
                    break;
                case OIOrderState.Checked:
                    orderState = OrderState.Checked;
                    break;
                case OIOrderState.BookingConfirmed:
                    orderState = OrderState.BookingConfirmed;
                    break;
                case OIOrderState.LoadVoyage:
                case OIOrderState.PODNotify:
                    orderState = OrderState.LoadVoyage;
                    break;
                case OIOrderState.Release:
                    orderState = OrderState.Release;
                    break;
                case OIOrderState.Closed:
                    orderState = OrderState.Closed;
                    break;
                case OIOrderState.PickUp:
                    orderState = OrderState.PickUp;
                    break;
                case OIOrderState.Return:
                    orderState = OrderState.Return;
                    break;
                default:
                    orderState = OrderState.Unknown;
                    break;
            }
            return orderState;
        }

        /// <summary>
        /// 海出订单状态 >>> 订单状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static OrderState ToOState(this OEOrderState input)
        {
            OrderState orderState = OrderState.Unknown;
            switch (input)
            {
                case OEOrderState.NewOrder:
                    orderState = OrderState.NewOrder;
                    break;
                case OEOrderState.Rejected:
                    orderState = OrderState.Rejected;
                    break;
                case OEOrderState.Checked:
                    orderState = OrderState.Checked;
                    break;
                case OEOrderState.BookingConfirmed:
                    orderState = OrderState.BookingConfirmed;
                    break;
                case OEOrderState.CancelBooking:
                    orderState = OrderState.CancelBooking;
                    break;
                case OEOrderState.LoadPreVoyage:
                    orderState = OrderState.LoadPreVoyage;
                    break;
                case OEOrderState.LoadVoyage:
                    orderState = OrderState.LoadVoyage;
                    break;
                case OEOrderState.Closed:
                    orderState = OrderState.Closed;
                    break;
                default:
                    orderState = OrderState.Unknown;
                    break;
            }
            return orderState;
        }

        /// <summary>
        /// 订单状态 >>> 海进订单状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static OIOrderState ToOIState(this OrderState input)
        {
            OIOrderState orderState = OIOrderState.Unknown;
            switch (input)
            {
                case OrderState.NewOrder:
                    orderState = OIOrderState.NewOrder;
                    break;
                case OrderState.Rejected:
                    orderState = OIOrderState.Rejected;
                    break;
                case OrderState.Checked:
                    orderState = OIOrderState.Checked;
                    break;
                case OrderState.BookingConfirmed:
                    orderState = OIOrderState.BookingConfirmed;
                    break;
                case OrderState.LoadVoyage:
                    orderState = OIOrderState.LoadVoyage;
                    break;
                case OrderState.Release:
                    orderState = OIOrderState.Release;
                    break;
                case OrderState.Closed:
                    orderState = OIOrderState.Closed;
                    break;
                case OrderState.PickUp:
                    orderState = OIOrderState.PickUp;
                    break;
                case OrderState.Return:
                    orderState = OIOrderState.Return;
                    break;
                default:
                    orderState = OIOrderState.Unknown;
                    break;
            }
            return orderState;
        }

        /// <summary>
        /// 订单状态 >>> 海出订单状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static OEOrderState ToOEState(this OrderState input)
        {
            OEOrderState orderState = OEOrderState.Unknown;
            switch (input)
            {
                case OrderState.NewOrder:
                    orderState = OEOrderState.NewOrder;
                    break;
                case OrderState.Rejected:
                    orderState = OEOrderState.Rejected;
                    break;
                case OrderState.Checked:
                    orderState = OEOrderState.Checked;
                    break;
                case OrderState.BookingConfirmed:
                    orderState = OEOrderState.BookingConfirmed;
                    break;
                case OrderState.CancelBooking:
                    orderState = OEOrderState.CancelBooking;
                    break;
                case OrderState.LoadPreVoyage:
                    orderState = OEOrderState.LoadPreVoyage;
                    break;
                case OrderState.LoadVoyage:
                    orderState = OEOrderState.LoadVoyage;
                    break;
                case OrderState.Closed:
                    orderState = OEOrderState.Closed;
                    break;
                default:
                    orderState = OEOrderState.Unknown;
                    break;
            }
            return orderState;
        }
    }
}

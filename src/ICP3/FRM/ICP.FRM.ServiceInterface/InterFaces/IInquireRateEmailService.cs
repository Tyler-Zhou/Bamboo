#region Comment

/*
 * 
 * FileName:    IInquireRateEmailService.cs
 * CreatedOn:   2014/6/13 11:48:08
 * CreatedBy:   taylor 
 * 
 * 
 * Description：
 *      ->询价邮件服务接口
 * History：
 * 
 * 
 * 
 * 
 */

#endregion


using ICP.FRM.ServiceInterface.DataObjects;
using System;
using System.Collections.Generic;


namespace ICP.FRM.ServiceInterface
{
    /// <summary>
    /// 询价邮件服务接口
    /// </summary>
    public interface IInquireRateEmailService
    {
        /// <summary>
        /// 发送询价结果到InquireBy
        /// </summary>
        /// <param name="inquireRateList">传入集合对象</param>
        void SendEmailToInquireBy(List<BaseInquireRate> inquireRateList);

        /// <summary>
        /// 发送询价询问信息到RespondBy
        /// </summary>
        /// <param name="paramInquireObj">传入对象</param>
        void SendEmailToRespondBy(BaseInquireRate paramInquireObj);

        /// <summary>
        /// 确认后通知
        /// </summary>
        /// <param name="BookingID">业务ID</param>
        /// <param name="paramInquireObj">询价对象</param>
        void MailBookingConfirm(Guid BookingID, BaseInquireRate paramInquireObj);
    }
}

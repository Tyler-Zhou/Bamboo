using ICP.Framework.CommonLibrary.Common;
using System;

namespace ICP.FCM.Common.ServiceInterface.DataObjects
{
    public static class EventCodeExtensionMethods
    {
        /// <summary>
        /// 转换成保存对象
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static EventObjects ConvertToEventObjects(this EventCode input)
        {
            EventObjects eventObj = new EventObjects()
            {
                Code = input.Code,
                Id = Guid.Empty,
                IsShowAgent = false,
                IsShowCustomer = true,
                IsShowCSPEvent = input.IsShowCSPEvent,
                IsShowCSPActivity = input.IsShowCSPActivity,
                Subject = input.Subject,
                Description = input.Subject,
                Priority = MemoPriority.Normal,
                Type = MemoType.Memo,
                UpdateDate = DateTime.Now,
            };
            return eventObj;
        }
    }
}

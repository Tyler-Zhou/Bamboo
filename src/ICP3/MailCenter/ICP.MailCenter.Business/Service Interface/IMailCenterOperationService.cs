using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ICP.ReportCenter.ServiceInterface;

namespace ICP.MailCenter.Business.ServiceInterface
{
    /// <summary>
    /// 邮件中心操作日志
    /// </summary>
    [ServiceContract]
    public interface IMailCenterOperationService
    {
        /// <summary>
        /// 保存邮件中心操作日志
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="useDay">0表示当天关闭邮件中心，1表示第二天关闭邮件中心</param>
        /// <returns></returns>
        [OperationContract]
        bool SaveOperationMemo(string userName, DateTime createDate, int useDay);
        /// <summary>
        /// 跟据使用时间来判断当前用户当天是否使用过邮件中心
        /// </summary>
        /// <param name="useTime"></param>
        /// <returns></returns>  
        [OperationContract]
        bool GetMailCenterOperationMemoByUseTime(string useTime);
        /// <summary>
        /// 获取邮件中心所有操作日志
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        [OperationContract]
        ICP.ReportCenter.ServiceInterface.DataObjects.ContactObject GetAllList(Guid companyId);
    }
}

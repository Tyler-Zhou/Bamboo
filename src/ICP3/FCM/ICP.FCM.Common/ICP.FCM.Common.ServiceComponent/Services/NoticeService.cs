using ICP.FCM.Common.ServiceInterface.DataObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ICP.FCM.Common.ServiceComponent
{
    /// <summary>
    /// 通知服务实现类
    /// </summary>
    public partial class FCMCommonService
    {
        /// <summary>
        /// 获取未确认AMS业务
        /// </summary>
        /// <returns></returns>
        public List<NoticeAMSBL> GetNoticeBLByAMSState()
        {
            try
            {
                return new List<NoticeAMSBL>();
            }
            catch (SqlException sqlException)
            {
                throw new ApplicationException(sqlException.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 未确认AMS通知
        /// </summary>
        public void NoticeUnconfirmedAMS()
        {
            
        }
    }
}

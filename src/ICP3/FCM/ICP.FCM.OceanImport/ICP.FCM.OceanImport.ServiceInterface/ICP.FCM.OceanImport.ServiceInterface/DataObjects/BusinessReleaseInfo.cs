using System;
using ICP.FCM.Common.ServiceInterface.DataObjects;

namespace ICP.FCM.OceanImport.ServiceInterface
{
    /// <summary>
    /// 业务放单信息
    /// </summary>
    [Serializable]
    public class BusinessReleaseInfo
    {
        /// <summary>
        /// 放单类型
        /// </summary>
        private FCMReleaseType releaseType;

        public FCMReleaseType ReleaseType
        {
            get { return releaseType; }
            set { releaseType = value; }
        }

        /// <summary>
        /// 更新时间
        /// </summary>
        private DateTime? updateDate;

        public DateTime? UpdateDate
        {
            get { return updateDate; }
            set { updateDate = value; }
        }

        /// <summary>
        /// 放货日期
        /// </summary>
        private DateTime? releasedate;

        public DateTime? Releasedate
        {
            get { return releasedate; }
            set { releasedate = value; }
        }

    }
}

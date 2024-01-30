using System;

namespace ICP.FAM.ServiceInterface.DataObjects.SaveRequests
{
    /// <summary>
    /// 保存后续业务信息类
    /// </summary>
    [Serializable]
    public class FollowBusinessSaveRequest
    {
        Guid _id;
        /// <summary>
        /// 唯一键
        /// </summary>
        public Guid ID
        {
            get;
            set;
        }
        /// <summary>
        /// C/N附件
        /// </summary>
        public string CNCopy
        {
            get;
            set;
        }

        /// <summary>
        /// MBL附件
        /// </summary>
        public string MBLCopy
        {
            get;
            set;
        }

        /// <summary>
        /// SO附件
        /// </summary>
        public string SOCopy
        {
            get;
            set;
        }
        /// <summary>
        /// SONO
        /// </summary>
        public string SONO
        {
            get;
            set;
        }

        /// <summary>
        /// 船名航次
        /// </summary>      
        public string VesselVoyage
        {
            get;
            set;
        }

        /// <summary>
        /// 选择
        /// </summary>
        public bool Selected
        {
            get;
            set;
        }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateDate
        {
            get;
            set;
        }

        /// <summary>
        /// 更新人
        /// </summary>
        public Guid UpdateBy
        {
            get;
            set;
        }

    }
}

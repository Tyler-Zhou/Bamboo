using System;

namespace ICP.FCM.Common.ServiceInterface.DataObjects
{
    #region 事件代码
    /// <summary>
    /// 事件代码
    /// </summary>
    public class EventCode
    {
        #region 唯一键
        /// <summary>
        /// 唯一键
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }
        #endregion

        #region 代码
        /// <summary>
        /// 代码
        /// </summary>
        public string Code
        {
            get;
            set;
        }
        #endregion

        #region 主题
        /// <summary>
        /// 主题
        /// </summary>
        public string Subject
        {
            get;
            set;
        }
        #endregion

        #region 类别
        /// <summary>
        /// 类别
        /// </summary>
        public string Category
        {
            get;
            set;
        }
        #endregion

        #region 显示代码
        /// <summary>
        /// 显示代码
        /// </summary>
        public string ShowCode
        {
            get;
            set;
        }
        #endregion

        #region 显示主题
        /// <summary>
        /// 显示主题(格式：CODE-SUBJECT)
        /// </summary>
        public string Show
        {
            get;
            set;
        }
        #endregion

        #region 是否显示到CSP事件
        /// <summary>
        /// 是否显示到CSP事件
        /// </summary>
        public bool IsShowCSPEvent
        {
            get;
            set;
        }
        #endregion

        #region 是否显示到CSP留言板
        /// <summary>
        /// 是否显示到CSP留言板
        /// </summary>
        public bool IsShowCSPActivity
        {
            get;
            set;
        }
        #endregion
    } 
    #endregion
}

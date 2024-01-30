using System.Collections.Generic;
using System.Data;
using ICP.DataCache.ServiceInterface;
using ICP.Message.ServiceInterface;

namespace ICP.MailCenterFramework.UI
{
    /// <summary>
    /// 邮件数据源
    /// </summary>
    public static class HelpMailStore
    {
        /// <summary>
        /// 全局邮件实体
        /// </summary>
        public static Message.ServiceInterface.Message msg { get; set; }

        /// <summary>
        /// 全局邮件EntryID
        /// </summary>
        public static string EntryID { get; set; }

        #region 联系人

        private static List<OperationContactInfo> _TableOperationContact;
        /// <summary>
        /// 联系人
        /// </summary>
        public static List<OperationContactInfo> TableOperationContact
        {
            get { return _TableOperationContact ?? (_TableOperationContact = new List<OperationContactInfo>()); }
            set { _TableOperationContact = value; }
        } 
        #endregion

        #region 关联信息
        private static List<OperationMessageRelation> _TableMessageRelation;

        /// <summary>
        ///关联信息 
        /// </summary>
        public static List<OperationMessageRelation> TableMessageRelation
        {
            get { return _TableMessageRelation ?? (_TableMessageRelation = new List<OperationMessageRelation>()); }
            set { _TableMessageRelation = value; }
        } 
        #endregion

        /// <summary>
        /// 业务信息
        /// </summary>
        public static DataTable TableBusiness { get; set; }

        /// <summary>
        /// 当前邮件实体对象
        /// </summary>
        public static object CurrentMailItem { get; set; }

        /// <summary>
        /// 是否需要登陆ICP
        /// </summary>
        public static bool IsNeedLoginICP { get; set; }

        #region 属性-能否注册快捷键
        /// <summary>
        /// 能否注册快捷键
        /// </summary>
        private static bool _CanRegisterHotKey = true;
        /// <summary>
        /// 能否注册快捷键
        /// </summary>
        public static bool CanRegisterHotKey
        {
            get { return _CanRegisterHotKey; }

            set
            {
                _CanRegisterHotKey = value;
            }
        }

        private static List<OperationMessageRelation> _currentMessageRelation;
        /// <summary>
        /// 全局缓存当前邮件的关联信息
        /// </summary>
        public static List<OperationMessageRelation> CurrentMessageRelation 
        {
            get { return _currentMessageRelation ?? new List<OperationMessageRelation>(); }
            set { _currentMessageRelation = value; }
        }
        #endregion
    }
}

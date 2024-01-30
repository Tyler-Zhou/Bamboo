#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/7/17 星期二 09:52:45
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.EDIManager.ServiceInterface
{
    /// <summary>
    /// 通用常量
    /// </summary>
    public class CommonConstants
    {
        

        #region Section
        /// <summary>
        /// Inttra FTP
        /// </summary>
        public const string SECTION_INTTRA_FTP = "InttraFTP";
        /// <summary>
        /// Inttra 船期
        /// </summary>
        public const string SECTION_INTTRA_SCHEDULE = "InttraSchedule";
        /// <summary>
        /// Inttra 邮件
        /// </summary>
        public const string SECTION_INTTRA_MAIL = "InttraMail";
        /// <summary>
        /// 发送EDI通知
        /// </summary>
        public const string SECTION_SENDEDINOTICE = "SendEDINotice";
        #endregion

        #region TaskSchedule
        /// <summary>
        /// 是否下载船期
        /// </summary>
        public const string TASKSCHEDULE_DOWNLOADSCHEDULE = "DownloadSchedule";
        /// <summary>
        /// 下载船期启动延迟时间
        /// </summary>
        public const string TASKSCHEDULE_DOWNLOADSCHEDULE_DELAYED = "DownloadScheduleDelayed";
        /// <summary>
        /// 下载船期间隔时间
        /// </summary>
        public const string TASKSCHEDULE_DOWNLOADSCHEDULE_INTERVAL = "DownloadScheduleInterval";
        /// <summary>
        /// 是否解析船期
        /// </summary>
        public const string TASKSCHEDULE_RESOLVESCHEDULE = "ResolveSchedule";
        /// <summary>
        /// 解析船期启动延迟时间
        /// </summary>
        public const string TASKSCHEDULE_RESOLVESCHEDULE_DELAYED = "ResolveScheduleDelayed";
        /// <summary>
        /// 解析船期间隔时间
        /// </summary>
        public const string TASKSCHEDULE_RESOLVESCHEDULE_INTERVAL = "ResolveScheduleInterval";
        /// <summary>
        /// 是否解析邮件
        /// </summary>
        public const string TASKSCHEDULE_RESOLVEEMAIL = "ResolveEmail";
        /// <summary>
        /// 解析邮件启动延迟时间
        /// </summary>
        public const string TASKSCHEDULE_RESOLVEEMAIL_DELAYED = "ResolveEmailDelayed";
        /// <summary>
        /// 解析邮件间隔时间
        /// </summary>
        public const string TASKSCHEDULE_RESOLVEEMAIL_INTERVAL = "ResolveEmailInterval";
        /// <summary>
        /// 是否发送EDI通知
        /// </summary>
        public const string TASKSCHEDULE_SENDEDINOTICE = "SendEDINotice";
        /// <summary>
        /// 发送通知启动延迟时间
        /// </summary>
        public const string TASKSCHEDULE_SENDEDINOTICE_DELAYED = "SendEDINoticeDelayed";
        /// <summary>
        /// 发送通知间隔时间
        /// </summary>
        public const string TASKSCHEDULE_SENDEDINOTICE_INTERVAL = "SendEDINoticeInterval";
        #endregion

        #region Inttra FTP
        /// <summary>
        /// Inttra FTP 用户名
        /// </summary>
        public const string INTTRA_FTP_USERID = "UserID";
        /// <summary>
        /// Inttra FTP 密码
        /// </summary>
        public const string INTTRA_FTP_PASSWORD = "Password";
        /// <summary>
        /// 船期抓取地址
        /// </summary>
        public const string INTTRA_FTP_SCHEDULEURL = "ScheduleUrl";
        
        #endregion

        #region Inttra Schedule
        /// <summary>
        /// 保存路径
        /// </summary>
        public const string INTTRASCHEDULE_SENDEDINOTICE = "SaveDirectory";
        /// <summary>
        /// 正则表达式-匹配船期文件名
        /// </summary>
        public const string INTTRASCHEDULE_REGEX_SCHEDULE_FILENAME = "RegexScheduleFileName";
        #endregion

        #region Inttra Mail
        /// <summary>
        /// 邮箱服务器
        /// </summary>
        public const string INTTRAMAIL_POPSERVER = "PopServer";
        /// <summary>
        /// 邮箱登录用户
        /// </summary>
        public const string INTTRAMAIL_LOGIN = "Login";
        /// <summary>
        /// 邮箱密码
        /// </summary>
        public const string INTTRAMAIL_PASSWORD = "Password";
        /// <summary>
        /// 已解析邮件数量
        /// </summary>
        public const string INTTRAMAIL_RESOLVEEMAILCOUNT = "ResolveEmailCount";
        /// <summary>
        /// 补料传输错误
        /// </summary>
        public const string INTTRAMAIL_REGEX_TSIERROR = "RegexTSIError";
        /// <summary>
        /// 补料传输错误详细
        /// </summary>
        public const string INTTRAMAIL_REGEX_TSIERRORCONTENT = "RegexTSIErrorContent";
        /// <summary>
        /// EDI失败错误
        /// </summary>
        public const string INTTRAMAIL_REGEX_TFERROR = "RegexTFError";
        /// <summary>
        /// EDI失败错误详细
        /// </summary>
        public const string INTTRAMAIL_REGEX_TFERRORCONTENT = "RegexTFErrorContent";
        /// <summary>
        /// 补料
        /// </summary>
        public const string INTTRAMAIL_REGEX_SI = "RegexSI";
        /// <summary>
        /// 补料内容
        /// </summary>
        public const string INTTRAMAIL_REGEX_SICONTENT = "RegexSIContent";
        /// <summary>
        /// 补料错误
        /// </summary>
        public const string INTTRAMAIL_REGEX_SISDR = "RegexSISDR";
        /// <summary>
        /// 补料错误内容
        /// </summary>
        public const string INTTRAMAIL_REGEX_SISDRCONTENT = "RegexSISDRContent";
        /// <summary>
        /// VGM
        /// </summary>
        public const string INTTRAMAIL_REGEX_VGM = "RegexVGM";
        /// <summary>
        /// VGM 内容
        /// </summary>
        public const string INTTRAMAIL_REGEX_VGMCONTENT = "RegexVGMContent";
        #endregion

        #region Send EDI Notice
        /// <summary>
        /// 近{0}天的数据
        /// </summary>
        public const string SENDEDINOTICE_BEFOREDAY = "BeforeDay";
        /// <summary>
        /// 提前{0}小时通知
        /// </summary>
        public const string SENDEDINOTICE_BEFOREHOUR = "BeforeHour";

        #endregion
    }
}

#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/1/22 16:53:20
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using System.Runtime.Serialization;

namespace ICP.Monitor.Model
{
    /// <summary>
    /// 系统自定义异常类
    /// </summary>
    public sealed class CustomException : Exception, ISerializable
    {
        #region Base
        /// <summary>
        /// 调用基类的构造器
        /// </summary>
        public CustomException()
            : base()
        {
        }
        /// <summary>
        /// 调用基类的构造器
        /// </summary>
        /// <param name="message"></param>
        public CustomException(string message)
            : base(message)
        {
        }
        /// <summary>
        /// 调用基类的构造器
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public CustomException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
        #endregion

        #region Extension
        /// <summary>
        /// 执行操作使用参数
        /// </summary>
        public string ExecParam { get; private set; }
        /// <summary>
        /// 是否仅用于提示异常
        /// </summary>
        public bool IsOnlyTips { get; set; }
        /// <summary>
        /// 原始StackTrace信息
        /// </summary>
        public string OriginalStackTrace { get; set; }
        /// <summary>
        /// 异常日志ID
        /// </summary>
        public string ErrorLogID { get; set; }
        /// <summary>
        /// 自定义异常信息描述
        /// </summary>
        public string ErrorMessage
        {
            get
            {
                string msg = "";
                //连接查询条件到异常信息
                if (!string.IsNullOrEmpty(ExecParam))
                    msg = string.Format("Parameters:\r\n{0}\r\n", ExecParam);
                //连接Message信息
                if (!string.IsNullOrEmpty(Message))
                    msg = string.Format("{0}Message:\r\n{1}\r\n", msg, Message);
                //连接StackTrace到异常信息
                if (!string.IsNullOrEmpty(OriginalStackTrace))
                    msg = string.Format("{0}Stack Trace:\r\n{1}\r\n", msg, OriginalStackTrace);
                return msg;
            }
        }

        /// <summary>
        /// 让基类反序列化其内定义的字段
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        /// <remarks>因为定义了至少新的字段，所以要定义一个特殊的构造器用于反序列化由于该类是密封类，所以该构造器的访问限制被定义为私有方式。否则，该构造器被定义为受保护方式</remarks>
        private CustomException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            ExecParam = info.GetString("ExecParam");
            IsOnlyTips = info.GetBoolean("IsOnlyTips");
            OriginalStackTrace = info.GetString("OriginalStackTrace");
            ErrorLogID = info.GetString("ErrorLogID");
        }
        /// <summary>
        /// 定义序列化方法
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        /// <remarks>因为定义了至少一个字段，所以要定义序列化方法</remarks>
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            //序列化新定义的每个字段
            info.AddValue("ExecParam", ExecParam);
            info.AddValue("IsOnlyTips", IsOnlyTips);
            info.AddValue("OriginalStackTrace", OriginalStackTrace);
            info.AddValue("ErrorLogID", ErrorLogID);
            //让基类序列化其内定义的字段
            base.GetObjectData(info, context);
        }
        /// <summary>
        /// 定义额外的构造器设置新定义的字段
        /// 调用另外一个构造器
        /// </summary>
        /// <param name="message"></param>
        /// <param name="execParam">扩展参数：执行操作使用参数</param>
        public CustomException(string message, string execParam)
            : this(message)
        {
            ExecParam = execParam;
        }

        /// <summary>
        /// 定义额外的构造器设置新定义的字段
        /// 调用另外一个构造器
        /// </summary>
        /// <param name="message"></param>
        /// <param name="execParam">扩展参数：执行操作使用参数</param>
        /// <param name="innerException">异常对象</param>
        public CustomException(string message, string execParam, Exception innerException)
            : this(message, innerException)
        {
            ExecParam = execParam;
            OriginalStackTrace = innerException.StackTrace;
        }
        #endregion

    }
}

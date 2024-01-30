#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/9/11 17:20:59
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


namespace Cityocean.Crawl.CommonLibrary
{
    /// <summary>
    /// 自定义异常类
    /// </summary>
    public sealed class CrawlException : Exception, ISerializable
    {
        #region Base
        /// <summary>
        /// 调用基类的构造器
        /// </summary>
        public CrawlException()
            : base()
        {
        }
        /// <summary>
        /// 调用基类的构造器
        /// </summary>
        /// <param name="message"></param>
        public CrawlException(string message)
            : base(message)
        {
        }
        /// <summary>
        /// 调用基类的构造器
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public CrawlException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
        #endregion

        #region Extension
        /// <summary>
        /// 异常类型
        /// </summary>
        public string ErrorType { get; private set; }

        /// <summary>
        /// 自定义异常信息描述
        /// </summary>
        public string ErrorMessage
        {
            get
            {
                string msg = "";
                //连接查询条件到异常信息
                if (!string.IsNullOrEmpty(ErrorType))
                    msg = string.Format("ErrorType:\r\n{0}\r\n", ErrorType);
                return msg;
            }
        }

        /// <summary>
        /// 让基类反序列化其内定义的字段
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        /// <remarks>因为定义了至少新的字段，所以要定义一个特殊的构造器用于反序列化由于该类是密封类，所以该构造器的访问限制被定义为私有方式。否则，该构造器被定义为受保护方式</remarks>
        private CrawlException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            ErrorType = info.GetString("ErrorType");
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
            info.AddValue("ErrorType", ErrorType);
            //让基类序列化其内定义的字段
            base.GetObjectData(info, context);
        }
        /// <summary>
        /// 定义额外的构造器设置新定义的字段
        /// 调用另外一个构造器
        /// </summary>
        /// <param name="message"></param>
        /// <param name="paramErrorType">扩展参数：异常ID</param>
        public CrawlException(string message, string paramErrorType)
            : this(message)
        {
            ErrorType = paramErrorType;
        }

        /// <summary>
        /// 定义额外的构造器设置新定义的字段
        /// 调用另外一个构造器
        /// </summary>
        /// <param name="message"></param>
        /// <param name="paramErrorType">扩展参数：异常ID</param>
        /// <param name="innerException">异常对象</param>
        public CrawlException(string message, string paramErrorType, Exception innerException)
            : this(message, innerException)
        {
            ErrorType = paramErrorType;
        }
        #endregion

    }
}

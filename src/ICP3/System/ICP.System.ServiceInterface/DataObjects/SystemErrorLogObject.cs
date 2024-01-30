using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.Sys.ServiceInterface
{
    /// <summary>
    /// 系统错误日志实体类
    /// </summary>
    public class SystemErrorLogObject
    {
        public SystemErrorLogObject()
        {
            this.Preview = LocalData.IsEnglish ? "Preview" : "预览";
        }

        public Guid ID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserID { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户当前登陆记录的Session的一个ID
        /// </summary>
        public Guid SessionID { get; set; }
        /// <summary>
        /// 错误截屏字节流
        /// </summary>
        public byte[] ScreenCapture { get; set; }
        /// <summary>
        /// 错误详细信息
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 错误发生的时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 预览
        /// </summary>
        public string Preview { get; set; }
    }
}

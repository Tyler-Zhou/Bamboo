using System;

namespace NB.CLP
{
    /// <summary>
    /// 请求结果
    /// </summary>
    [Serializable]
    public class Result
    {
        /// <summary>
        /// 编码																		
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 消息																		
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 明细
        /// </summary>
        public ResultDetail data { get; set; }


    }

    /// <summary>
    /// 请求结果
    /// </summary>
    [Serializable]
    public class ResultDetail
    {
        /// <summary>
        /// 结果																		
        /// </summary>
        public string result { get; set; }
        /// <summary>
        /// 错误信息																		
        /// </summary>
        public string errorInfo { get; set; }
        /// <summary>
        /// 文档内容
        /// </summary>
        public string docContent { get; set; }
    }
}

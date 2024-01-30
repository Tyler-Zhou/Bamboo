using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.Common.ServiceInterface.DataObjects
{
    /// <summary>
    /// 数字识别结果
    /// </summary>
    public class NumberResult
    {
        /// <summary>
        /// 识别ID
        /// </summary>
        public string log_id { get; set; }
        /// <summary>
        /// 识别结果
        /// </summary>
        public List<WordsResult> words_result { get; set; }
        /// <summary>
        /// 识别到的数量
        /// </summary>
        public int words_result_num { get; set; }
    }
    /// <summary>
    /// 位置结果
    /// </summary>
    public class WordsResult
    {
        /// <summary>
        /// 位置
        /// </summary>
        public NumberLocation location { get; set; }
        /// <summary>
        /// 识别结果
        /// </summary>
        public string words { get; set; }
    }
    /// <summary>
    /// 单个坐标识别信息
    /// </summary>
    public class NumberLocation
    {
        /// <summary>
        /// 左坐标
        /// </summary>
        public int left { get; set; }
        /// <summary>
        /// 上坐标
        /// </summary>
        public int top { get; set; }
        /// <summary>
        /// 宽
        /// </summary>
        public int width { get; set; }
        /// <summary>
        /// 高
        /// </summary>
        public int height { get; set; }
    }
}

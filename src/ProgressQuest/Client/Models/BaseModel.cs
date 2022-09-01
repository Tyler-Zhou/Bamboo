using Newtonsoft.Json;
using System;

namespace Client.Models
{
    /// <summary>
    /// 模型基类
    /// </summary>
    public class BaseModel
    {
        /// <summary>
        /// 键值
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [JsonIgnore]
        public virtual string Name
        {
            get
            {
                string name = Key;
                try
                {
                    name = System.Windows.Application.Current.FindResource(Key).ToString();
                }
                catch (Exception ex)
                {
                    errorMessage =$"获取[{Key}]出现异常:{ex.Message}";
                    name = Key;
                }
                return name;
            }
        }

        private string errorMessage = string.Empty;
        /// <summary>
        /// 错误信息
        /// </summary>
        [JsonIgnore]
        public string ErrorMessage
        { 
            get
            {
                return errorMessage;
            }
        }
    }
}

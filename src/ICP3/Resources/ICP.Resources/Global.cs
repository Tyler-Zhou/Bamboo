using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.Resources
{
    /// <summary>
    /// 资源全局类
    /// </summary>
    public class Global
    {
        static Dictionary<string, int> _values = new Dictionary<string, int>();

        static Global()
        {
            foreach (string str in new string[] { "A", "B", "C", "D", "E", "F", "G",
                "H", "I", "J", "K", "L", "M", "N", 
                "O", "P", "Q", "R", "S", "T",
                "U", "V", "W", "X", "Y", "Z" })
            {

                int number = Convert.ToInt32(str + "0000", 16);
                _values.Add(str, number);
            }
        }

        /// <summary>
        /// 海运出口模块
        /// </summary>
        static public OceanExport OceanExport{get;private set;}

        /// <summary>
        /// 海运进口模块
        /// </summary>
        static public OceanImport OceanImport { get; private set; }

        /// <summary>
        /// 根据消息代码获取提示信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        static public string GetMessage(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                throw new NullReferenceException("Resource code must not be null.");
            }
            code = code.Trim();

            if (code.Length != 5)
            {
                throw new ArgumentException("The message code length must be 5.");
            }

            string prefix = code.Substring(0, 1);

            return GetMessage(prefix, "M_" + code);
        }

        /// <summary>
        /// 根据消息代码获取提示信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        static private string GetMessage(string prefix,string code)
        {
            string message = string.Empty;

            try
            {
                switch (prefix)
                {
                    case "A":
                        message = Properties.Resources.ResourceManager.GetString(code);
                        break;
                    case "B":
                        message = Basic.ResourceManager.GetString(code);
                        break;
                    case "F":
                        message = Finance.ResourceManager.GetString(code);
                        break;
                    case "O":
                        message = OceanExport.ResourceManager.GetString(code);
                        break;
                    case "I":
                        message = OceanImport.ResourceManager.GetString(code);
                        break;
                    default:
                        message = Framework.ResourceManager.GetString(code);
                        break;
                }
            }
            catch
            {
                message = "Null resource";
            }

            return message;
        }
    }
}

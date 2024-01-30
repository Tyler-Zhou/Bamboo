#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/3/29 11:50:33
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;

namespace Cityocean.Crawl.CommonLibrary
{
    /// <summary>
    /// INI配置文件
    /// </summary>
    public sealed class INIHelper
    {
        #region Fields
        /// <summary>
        /// Path to the physical path of the ini file
        /// </summary>
        private string strPath;
         // 定义一个静态变量来保存类的实例
        private static readonly INIHelper _instance = new INIHelper(GlobalVariable.ConfigPath);
        #endregion

        #region Constructor
        // 定义私有构造函数，使外界不能创建该类实例
        INIHelper()
        {
        }
        static INIHelper()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramPathFileName">文件名称(含路径)</param>
        INIHelper(string paramPathFileName)
        {
            strPath = paramPathFileName;
        }
        /// <summary>
        /// 定义公有方法提供一个全局访问点,同时你也可以定义公有属性来提供全局访问点
        /// </summary>
        /// <returns></returns>
        public static INIHelper Instance
        {
            get
            {
                return _instance;
            }
        }
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramPath">文件路径</param>
        /// <param name="paramFileName">文件名称</param>
        public INIHelper(string paramPath, string paramFileName)
        {
            strPath = paramPath + @"\" + paramFileName + ".ini";
        }
        #endregion

        #region Statement to read and write INI file API function
        /// <summary>
        /// The overloaded function GetPrivateProfileString DLL export
        /// string
        /// </summary>
        /// <param name="section">Paragraph name to be read</param>
        /// <param name="key">Key to read</param>
        /// <param name="val">String to read</param>
        /// <param name="filePath">The full path and file name of the INI file</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        /// <summary>
        /// The overloaded function GetPrivateProfileString DLL export
        /// StringBuilder
        /// </summary>
        /// <param name="section">Paragraph name to be read</param>
        /// <param name="key">Key to read</param>
        /// <param name="defVal">Reads the exception of the case of the default Value</param>
        /// <param name="retVal">This parameter type is not a string, but StringBuilder return type string section group or KeyValue group of.</param>
        /// <param name="size">Value allowable size</param>
        /// <param name="filePath">The full path and file name of the INI file</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string defVal, StringBuilder retVal, int size, string filePath);

        /// <summary>
        /// The overloaded function GetPrivateProfileString DLL export
        /// Byte []
        /// </summary>
        /// <param name="section">Paragraph name to be read</param>
        /// <param name="key">Key to read</param>
        /// <param name="defVal">Reads the exception of the case of the default Value</param>
        /// <param name="retVal">This parameter type is not a string, but Byte [] return type byte section group or KeyValue group of.</param>
        /// <param name="size">Value allowable size</param>
        /// <param name="filePath">The full path and file name of the INI file</param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string defVal, Byte[] retVal, int size, string filePath);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lpszReturnBuffer"></param>
        /// <param name="nSize"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileSectionNames", CharSet = CharSet.Ansi)]
        public static extern int GetPrivateProfileSectionNames(IntPtr lpszReturnBuffer, int nSize, string filePath);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lpAppName"></param>
        /// <param name="lpReturnedString"></param>
        /// <param name="nSize"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [DllImport("KERNEL32.DLL ", EntryPoint = "GetPrivateProfileSection", CharSet = CharSet.Ansi)]
        public static extern int GetPrivateProfileSection(string lpAppName, byte[] lpReturnedString, int nSize, string filePath);
        #endregion

        #region Public Method
        /// <summary>
        /// Write INI File
        /// </summary>
        /// <param name="section">Paragraph</param>
        /// <param name="key">Key</param>
        /// <param name="iValue">Value</param>
        public void IniWriteValue(string section, string key, string iValue)
        {
            WritePrivateProfileString(section, key, iValue, strPath);
        }
        /// <summary>
        /// Read INI file
        /// Return string
        /// </summary>
        /// <param name="section">Paragraph</param>
        /// <param name="key">Key</param>
        /// <returns>Return KeyValue</returns>
        public string IniReadValue(string section, string key)
        {
            StringBuilder temp = new StringBuilder(65534);
            int i = GetPrivateProfileString(section, key, "", temp, 65534, strPath);
            return "" + temp;
        }
        /// <summary>
        /// Read INI file
        /// Return byte[]
        /// </summary>
        /// <param name="section">Paragraph</param>
        /// <param name="key">Key</param>
        /// <returns>Back to byte type section group or KeyValue group</returns>
        public byte[] IniReadValues(string section, string key)
        {
            byte[] temp = new byte[255];
            int i = GetPrivateProfileString(section, key, "", temp, 255, strPath);
            return temp;
        }
        /// <summary>
        /// 读取一个ini里面所有的节,以数组输出
        /// </summary>
        /// <param name="sections">Paragraph</param>
        /// <returns>-1:失败;0:成功</returns>
        public int GetAllSectionNames(out string[] sections)
        {
            int MAX_BUFFER = 32767;
            IntPtr pReturnedString = Marshal.AllocCoTaskMem(MAX_BUFFER);
            int bytesReturned = GetPrivateProfileSectionNames(pReturnedString, MAX_BUFFER, strPath);
            if (bytesReturned == 0)
            {
                sections = null;
                return -1;
            }
            string local = Marshal.PtrToStringAnsi(pReturnedString, (int)bytesReturned).ToString();
            Marshal.FreeCoTaskMem(pReturnedString);
            //use of Substring below removes terminating null for split
            sections = local.Substring(0, local.Length - 1).Split('\0');
            return 0;
        }
        /// <summary>
        /// 获取段落下所有节点,以Hashtable输出
        /// </summary>
        /// <param name="section">Paragraph</param>
        /// <param name="resultArray">Hashtable</param>
        /// <returns>-1:失败;0:成功</returns>
        public int GetAllKeyValues(string section, out Hashtable resultArray)
        {
            resultArray = new Hashtable();
            byte[] b = new byte[65535];

            GetPrivateProfileSection(section, b, b.Length, strPath);
            string s = System.Text.Encoding.Default.GetString(b);
            string[] tmp = s.Split((char)0);
            ArrayList result = new ArrayList();
            foreach (string r in tmp)
            {
                if (r != string.Empty)
                    result.Add(r);
            }
            for (int i = 0; i < result.Count; i++)
            {
                string[] item = result[i].ToString().Split(new char[] { '=' });
                if (item.Length == 2)
                {
                    resultArray.Add(item[0].Trim(), item[1].Trim());
                }
                else if (item.Length == 1)
                {
                    resultArray.Add(item[0].Trim(), "");
                }
                else if (item.Length == 0)
                {
                    resultArray.Add("", "");
                }
            }

            return 0;
        }
        /// <summary>
        /// 得到某个节点下面所有的key和value组合
        /// </summary>
        /// <param name="section">Paragraph</param>
        /// <param name="keys">Out Key</param>
        /// <param name="values">Out Value</param>
        /// <returns>-1:失败;0:成功</returns>
        public int GetAllKeyValues(string section, out string[] keys, out string[] values)
        {

            byte[] b = new byte[65535];

            GetPrivateProfileSection(section, b, b.Length, strPath);
            string s = System.Text.Encoding.Default.GetString(b);
            string[] tmp = s.Split((char)0);
            ArrayList result = new ArrayList();
            foreach (string r in tmp)
            {
                if (r != string.Empty)
                    result.Add(r);
            }
            keys = new string[result.Count];
            values = new string[result.Count];
            for (int i = 0; i < result.Count; i++)
            {
                string[] item = result[i].ToString().Split(new char[] { '=' });
                if (item.Length == 2)
                {
                    keys[i] = item[0].Trim();
                    values[i] = item[1].Trim();
                }
                else if (item.Length == 1)
                {
                    keys[i] = item[0].Trim();
                    values[i] = "";
                }
                else if (item.Length == 0)
                {
                    keys[i] = "";
                    values[i] = "";
                }
            }

            return 0;
        }
        #endregion
    }
}

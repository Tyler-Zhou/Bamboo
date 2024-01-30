#region Comment

/*
 * 
 * FileName:    ToolUtility.cs
 * CreatedOn:   2014/8/19 14:36:49
 * CreatedBy:   taylor 
 * 
 * 
 * Description：
 *      ->
 * History：
 *      ->
 * 
 * 
 * 
 * 
 */

#endregion

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.MailCenterFramework.UI
{
    /// <summary>
    /// 
    /// </summary>
    public static class ToolUtility
    {
        #region 处理对象

        /// <summary>
        /// 深拷贝,通过序列化对象再反序列化得出新的对象
        /// </summary>
        public static T
            Clone<T>(T t)
        {
            T clone;
            System.Xml.Linq.XDocument doc = new System.Xml.Linq.XDocument();

            System.Xml.XmlWriter w = doc.CreateWriter();
            //w.Settings.Encoding = System.Text.UnicodeEncoding.Unicode;
            System.Xml.Serialization.XmlSerializer s = new System.Xml.Serialization.XmlSerializer(typeof(T));
            s.Serialize(w, t);
            w.Flush();
            w.Close();
            clone = (T)s.Deserialize(doc.CreateReader());

            return clone;
        }

        #endregion

        #region 对象属性值的Copy

        /// <summary>
        /// 把source对象的值拷贝到一个类型为targetType的对象,然后返回产生的对象
        /// </summary>
        /// <param name="source">源对象</param>
        /// <param name="targetType">产生新的对象的类型</param>
        /// <returns>返回新的对象</returns>
        public static object CloneValue(object source, Type targetType)
        {
            object targe = Activator.CreateInstance(targetType);
            Type sourceType = source.GetType();
            PropertyInfo[] properties = sourceType.GetProperties();
            foreach (PropertyInfo p in properties)
            {
                PropertyInfo tp = targetType.GetProperty(p.Name);

                if (tp == null) continue;

                if (p.PropertyType.IsValueType
                    || p.PropertyType.Name == "String")//值类型
                {
                    tp.SetValue(targe, p.GetValue(source, null), null);
                }
                else if (p.PropertyType.IsGenericType
                    && p.PropertyType.GetInterface(typeof(System.Collections.IList).FullName) != null)//如果是泛集合
                {
                    //不处理

                    object pValue = p.GetValue(source, null);
                    foreach (object obj in (pValue as System.Collections.IEnumerable))
                    {
                        //如果未实例化集合属性则实例化它
                        if (tp.GetValue(targe, null) == null) tp.SetValue(targe, Activator.CreateInstance(tp.PropertyType), null);
                        //获取Item属性的类型
                        string subitemTypeName = System.Text.RegularExpressions.Regex.Match(tp.PropertyType.FullName, @"\[\[(?<val>.*?)\]\]").Groups["val"].Value;
                        Type subitemType = Type.GetType(subitemTypeName);
                        object temsubObj = null;
                        //子集属性类型为源类型-防止递归死掉
                        if (obj.GetType().FullName != sourceType.FullName)
                        {
                            temsubObj = CloneValue(obj, subitemType);
                        }
                        else
                        {
                            temsubObj = source;
                        }
                        System.Collections.IList temTarget = tp.GetValue(targe, null) as System.Collections.IList;
                        temTarget.Add(temsubObj);
                    }
                }
                else if (p.PropertyType.IsClass)
                {
                    //子集属性类型为源类型-防止递归死掉
                    if (p.PropertyType.FullName != sourceType.FullName)
                    {
                        object temSource = p.GetValue(source, null);
                        tp.SetValue(targe, CloneValue(temSource, tp.PropertyType), null);
                    }
                    else
                    {
                        tp.SetValue(targe, source, null);
                    }
                }
            }
            return targe;
        }
        #endregion

        #region File Operation
        public static Byte[] ReadFileContentFromDisk(String filePath)
        {
            if (!File.Exists(filePath))
            {
                WriteLog("Utility ReadFileContentFromDisk(String)", new Exception(filePath));
                return new byte[0];
            }
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                byte[] content = new byte[(int)fs.Length];
                fs.Read(content, 0, content.Length);
                return content;
            }

        } 
        #endregion

        public static void KillProcess(string procName)
        {
            Process[] arrProcess = Process.GetProcessesByName(procName);
            if (arrProcess != null)
            {
                if (arrProcess.Length > 1)
                {
                    for (int i = 1; i < arrProcess.Length; i++)
                    {
                        arrProcess[i].Kill();
                    }
                }
                arrProcess = null;
            }
        }

        /// <summary>
        /// 释放组件对象
        /// </summary>
        /// <param name="comObject"></param>
        public static void ReleaseComObject(object comObject)
        {
            if (comObject != null)
            {
                Marshal.ReleaseComObject(comObject);
                comObject = null;
            }
        }

        #region Log
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="mTitle">Title</param>
        /// <param name="ex">Exception</param>
        public static void WriteLog(string mTitle, Exception ex)
        {
            try
            {
                System.Text.StringBuilder strException = new System.Text.StringBuilder();
                strException.AppendFormat("============================ MailCenterFramework DateTime:{0}============================\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                strException.AppendFormat("Method:{0}\r\n", mTitle);
                strException.AppendFormat("Message:{0}\r\n", ex.Message);
                //ex.StackTrace为空时为自定义异常
                if (!string.IsNullOrEmpty(ex.StackTrace))
                    strException.AppendFormat("StackTrace:{0}\r\n", ex.StackTrace);
                strException.AppendFormat(ex.ToString());
                strException.Append("========================================================");
                Logger.Log.Info(strException.ToString());
            }
            catch
            {
            }
        }

        /// <summary>
        /// 临时写日志
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="strLong"></param>
        public static void TempWriteLog(string logType,string strLong)
        {
            try
            {
                string logPath = (string.IsNullOrEmpty(LocalData.MainPath) ? "C:" : LocalData.MainPath) + string.Format("\\LogFiles\\{0:yyyy-MM-dd}\\", DateTime.Now);
                System.IO.Directory.CreateDirectory(logPath);
                string FileName = String.Format("MailCenter{0}", logType) + ".txt";
                string FileFullPath = logPath + FileName;
                string WriteText = string.Empty;
                using (TextWriter write = File.AppendText(FileFullPath))
                {
                    write.WriteLine(strLong);
                    write.Close();
                }
            }
            catch
            {
            }
        } 
        #endregion
    }
}

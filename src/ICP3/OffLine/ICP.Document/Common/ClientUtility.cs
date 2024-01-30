#region Comment

/*
 * 
 * FileName:    ClientUtility.cs
 * CreatedOn:   2014/5/16 星期五 11:19:40
 * CreatedBy:   taylor
 * 
 * 
 * Description：
 *      ->客户端公用操作库
 * History：
 * 
 * 
 * 
 * 
 */

#endregion

using System;
using System.Diagnostics;
using System.IO;

namespace ICP.Document
{
    /// <summary>
    /// 客户端公用操作库
    /// </summary>
    public class ClientUtility
    {
        #region 属性
        static int height = 10;
        public static int Height
        {
            get { return height; }
            set { height = value; }
        }
        /// <summary>
        /// A list of dangerous characters
        /// </summary>
        private static char[] DangerCode = new char[] { '\'' };

        #endregion

        

        #region 通过进程名称得到进程对象
        /// <summary>
        /// 进程名称
        /// </summary>
        /// <param name="paramProcessName"></param>
        /// <returns></returns>
        public static Process GetProcessByName(string paramProcessName)
        {
            Process[] processes = Process.GetProcessesByName(paramProcessName);
            //遍历与当前进程名称相同的进程列表  
            foreach (Process process in processes)
            {
                //如果实例已经存在则忽略当前进程  
                if (process.ProcessName.Equals(paramProcessName))
                {
                    //返回已经存在的进程
                    return process;
                }
            }
            return null;
        }
        #endregion

        #region Verify that contain dangerous characters
        /// <summary>
        /// Verify that contain dangerous characters
        /// </summary>
        /// <param name="input">Authentication string</param>
        /// <returns></returns>
        public static bool HasDangerCode(string input)
        {
            for (int i = 0; i < DangerCode.Length; i++)
            {
                if (input.IndexOf(DangerCode[i]) >= 0)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region 验证文本控件值的有效性
        /// <summary>
        /// 验证文本控件值的有效性
        /// </summary>
        /// <typeparam name="FT">窗体类型</typeparam>
        /// <param name="frm">窗体实例</param>
        /// <param name="controlname">指定控件名称</param>
        /// <param name="ep">错误提示控件</param>
        /// <returns></returns>
        public static bool CheckTextValidated<FT>(FT frm, string controlname, System.Windows.Forms.ErrorProvider ep)
        {
            try
            {
                Type ParentType = frm.GetType();
                System.Reflection.FieldInfo fi = ParentType.GetField(controlname, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                if (fi != null)
                {
                    System.Windows.Forms.Control ctrl = (System.Windows.Forms.Control)fi.GetValue(frm);

                    string text = ctrl.Text.Trim();
                    if (text == "" || text == "[无数据]")
                    {
                        if (ep != null)
                        {
                            ep.SetError(ctrl, "必填");
                        }
                        ctrl.Focus();
                        return false;
                    }

                    if (HasDangerCode(text))
                    {
                        if (ep != null)
                        {
                            ep.SetError(ctrl, "包含危险字符");
                        }
                        ctrl.Focus();
                        return false;
                    }

                    if (ep != null)
                    {
                        ep.SetError(ctrl, "");
                    }
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
        #endregion

        #region 保存文件到磁盘(Content)
        /// <summary>
        /// 保存文件到磁盘(Content)
        /// </summary>
        /// <param name="item">文档对象</param>
        /// <returns>文件保存路径</returns>
        public static string SaveFileContentToDisk(DocumentInfo item)
        {
            return InnerSaveFileToDisk(item, false);
        } 
        #endregion

        #region 保存文件到磁盘(HtmlContent)
        /// <summary>
        /// 保存文件到磁盘(HtmlContent)
        /// </summary>
        /// <param name="item">文档对象</param>
        /// <returns>文件保存路径</returns>
        public static string SaveFileHtmlContentToDisk(DocumentInfo item)
        {
            return InnerSaveFileToDisk(item, true);
        } 
        #endregion

        #region 保存文件到磁盘(返回文件路径)
        /// <summary>
        /// 保存文件到磁盘(返回文件路径)
        /// </summary>
        /// <param name="item">文档对象</param>
        /// <param name="isHtmlCopy">是否Html</param>
        /// <returns>文件保存路径</returns>
        private static String InnerSaveFileToDisk(DocumentInfo item, bool isHtmlCopy)
        {
            string rootPath = isHtmlCopy ? IOHelper.HtmlTempPath : IOHelper.ContentTempPath;
            String tempDirectoryPath = Path.Combine(rootPath, item.Id.ToString());
            IOHelper.EnsureDirectoryExists(tempDirectoryPath);
            String filePath = Path.Combine(tempDirectoryPath, item.DName);

            IOHelper.WriteToDisk(filePath, item.Content);

            return filePath;
        } 
        #endregion
    }
}

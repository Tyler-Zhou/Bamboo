using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using DevExpress.Utils;
using ICP.Framework.ClientComponents.UIFramework;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using ICP.Message.ServiceInterface;
using Microsoft.Win32;

namespace ICP.OA.UI
{
    public class Utility
    {   
       
        #region 处理对象

        /// <summary>
        /// 深拷贝,通过序列化对象再反序列化得出新的对象
        /// </summary>
        public static T Clone<T>(T t)
        {
            T clone;
            System.Xml.Linq.XDocument doc = new System.Xml.Linq.XDocument();

            System.Xml.XmlWriter w = doc.CreateWriter();

            System.Xml.Serialization.XmlSerializer s = new System.Xml.Serialization.XmlSerializer(typeof(T));
            s.Serialize(w, t);
            w.Flush();
            w.Close();
            clone = (T)s.Deserialize(doc.CreateReader());

            return clone;
        }

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

        /// <summary>
        /// 把source对象的值拷贝到一个类型为targetType的对象,然后返回产生的对象


        /// </summary>
        /// <param name="source">源对象</param>
        /// <param name="targetType">需要拷贝到的对象(必须是targetType类型)</param>
        /// <param name="targetType">产生新的对象的类型</param>
        /// <returns>返回新的对象</returns>
        public static void CopyToValue(object source, object targe, Type targetType)
        {
            Type sourceType = source.GetType();
            PropertyInfo[] properties = sourceType.GetProperties();
            foreach (PropertyInfo p in properties)
            {
                PropertyInfo tp = targetType.GetProperty(p.Name);

                if (tp == null) continue;

                if (p.PropertyType.IsValueType
                    || p.PropertyType.Name == "String")//值类型

                {
                    try
                    {
                        tp.SetValue(targe, p.GetValue(source, null), null);
                    }
                    catch
                    {
                    }
                }
                else if (p.PropertyType.IsGenericType
                    && p.PropertyType.GetInterface(typeof(System.Collections.IList).FullName) != null)//如果是泛集合
                {
                    //不处理



                    try
                    {
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
                    catch
                    {
                    }
                }
                else if (p.PropertyType.IsClass)
                {
                    try
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
                    catch
                    {
                    }
                }
            }
            // return targe;
        }
        #endregion

        /// <summary>
        /// 获取对象某一属性值的ToString,为空的引用类型返回sting.Empty
        /// </summary>
        public static string GetObjectPropertyValue<T>(T t, string propertyname)
        {
            Type type = typeof(T);

            PropertyInfo property = type.GetProperty(propertyname);

            if (property == null) return string.Empty;

            object o = property.GetValue(t, null);

            if (o == null) return string.Empty;

            return o.ToString();
        }

        /// <summary>
        /// 比效两个对象是否相等,返回不相等的属性名
        /// </summary>
        public static string GetObjectUpdatePropertys<T>(T t1, T t2)
        {
            return GetObjectUpdatePropertys<T>(t1, t2, null);
        }

        /// <summary>
        /// 比效两个对象是否相等,返回不相等的属性名,ignores参数为不需要检测的属性

        /// </summary>
        public static string GetObjectUpdatePropertys<T>(T t1, T t2, List<string> ignores)
        {
            string info = string.Empty;

            Type type = typeof(T);

            PropertyInfo[] propertys = type.GetProperties();

            foreach (PropertyInfo property in propertys)
            {
                if (property.PropertyType.IsValueType || property.PropertyType.Name == "String")
                {
                    //if (property.PropertyType.FullName.Contains("Guid")) continue;
                    if (ignores != null && ignores.Count > 0 && ignores.Contains(property.Name)) continue;
                    object o1 = property.GetValue(t1, null);
                    object o2 = property.GetValue(t2, null);
                    string str1 = o1 == null ? string.Empty : o1.ToString();
                    string str2 = o2 == null ? string.Empty : o2.ToString();

                    if (str1 != str2) info += property.Name + ",";
                }
            }

            if (!string.IsNullOrEmpty(info)) info = info.Substring(0, info.Length - 1);

            return info;
        }


        /// <summary>
        /// 比效两个对象是否相等
        /// </summary>
        public static bool CheckObjectIsUpdate<T>(T t1, T t2)
        {
            string info = string.Empty;

            Type type = typeof(T);

            PropertyInfo[] propertys = type.GetProperties();

            foreach (PropertyInfo property in propertys)
            {
                if (property.PropertyType.IsValueType || property.PropertyType.Name == "String")
                {
                    object o1 = property.GetValue(t1, null);
                    object o2 = property.GetValue(t2, null);
                    string str1 = o1 == null ? string.Empty : o1.ToString();
                    string str2 = o2 == null ? string.Empty : o2.ToString();

                    if (str1 != str2) return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 比效两个对象是否相等,ignores参数为不需要检测的属性

        /// </summary>
        public static bool CheckObjectIsUpdate<T>(T t1, T t2, List<string> ignores)
        {
            string info = string.Empty;

            Type type = typeof(T);

            PropertyInfo[] propertys = type.GetProperties();

            foreach (PropertyInfo property in propertys)
            {
                if (property.PropertyType.IsValueType || property.PropertyType.Name == "String")
                {
                    if (ignores != null && ignores.Count > 0 && ignores.Contains(property.Name)) continue;

                    object o1 = property.GetValue(t1, null);
                    object o2 = property.GetValue(t2, null);
                    string str1 = o1 == null ? string.Empty : o1.ToString();
                    string str2 = o2 == null ? string.Empty : o2.ToString();

                    if (str1 != str2) return true;
                }
            }

            return false;
        }

        #endregion

        #region 处理值类型


        public static bool GuidIsNullOrEmpty(Guid? id)
        {
            if (id == null || id.Value == Guid.Empty) return true;
            else return false;
        }

        public static DateTime GetEndDate(DateTime date)
        {
            string dateStr = date.ToShortDateString();
            dateStr += " 23:59:59";
            return DateTime.Parse(dateStr);
        }

        #endregion

        #region dialog

        /// <summary>
        /// 弹出一个是提示"是否删除"的对话框
        /// </summary>
        /// <returns></returns>
        public static bool EnquireIsDeleteCurrentData()
        {
            DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Srue Delete Current Data?" : "是否删除当前数据?",
                                                 LocalData.IsEnglish ? "Tip" : "提示",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 弹出一个是提示" 删除此项会删除此项的所有子项,是否删除"的对话框
        /// </summary>
        /// <returns></returns>
        public static bool EnquireIsDeleteCurrentDataByHasChild()
        {


            DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Delete Current Data Will Delete Current Data All Child.,Srue Delete?" : "删除此项会删除此项的所有子项,是否删除?",
                                                 LocalData.IsEnglish ? "Tip" : "提示",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                return true;
            else
                return false;
        }


        /// <summary>
        /// 弹出一个是提示"作废节点会作废此节点下所有子节点，是否继续"的对话框
        /// </summary>
        /// <returns></returns>
        public static bool EnquireIsDisuseCurrentData()
        {

            DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Disuse current data will be Disuse this data all childs,sruc continue?"
                                                                        : "作废节点会作废此节点下所有子节点，是否继续?",
                                                 LocalData.IsEnglish ? "Tip" : "提示",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 弹出一个是提示"作废节点会作废此节点下所有子节点，是否继续"的对话框
        /// </summary>
        /// <returns></returns>
        public static bool EnquireIsAvailableCurrentData()
        {


            DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Available current data will be available this data all parents,sruc continue?"
                                                                        : "激活节点会激活此节点所有父节点，是否继续?",
                                                 LocalData.IsEnglish ? "Tip" : "提示",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 弹出一个是提示"是否保存"的对话框
        /// </summary>
        /// <returns></returns>
        public static bool EnquireIsSaveCurrentData()
        {

            DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Srue Save Data?" : "是否保存数据?",
                                                 LocalData.IsEnglish ? "Tip" : "提示",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 弹出一个是提示"数据有更改,是否保存数据"的对话框
        /// </summary>
        /// <returns></returns>
        public static DialogResult EnquireIsSaveCurrentDataByUpdated()
        {
            return Utility.EnquireIsSaveCurrentDataByUpdated(null);
        }

        /// <summary>
        /// 弹出一个是提示"数据有更改,是否保存数据"的对话框
        /// </summary>
        /// <returns></returns>
        public static DialogResult EnquireIsSaveCurrentDataByUpdated(string titel)
        {
            if (string.IsNullOrEmpty(titel)) titel = LocalData.IsEnglish ? "Tip" : "提示";

            DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "The data was changed,Sure save?" : "数据有更改是否保存?"
                                , titel
                                , MessageBoxButtons.YesNoCancel
                                , MessageBoxIcon.Question);
            return result;
        }

        /// <summary>
        /// 弹出一个是提示"新增的数据未保存,是否保存"的对话框
        /// </summary>
        /// <returns></returns>
        public static DialogResult EnquireIsSaveCurrentDataByNew()
        {
            return Utility.EnquireIsSaveCurrentDataByNew(null);
        }

        /// <summary>
        /// 弹出一个是提示"新增的数据未保存,是否保存"的对话框
        /// </summary>
        /// <returns></returns>
        public static DialogResult EnquireIsSaveCurrentDataByNew(string titel)
        {
            if (string.IsNullOrEmpty(titel)) titel = LocalData.IsEnglish ? "Tip" : "提示";

            DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "The new data is UnSave,Sure save?" : "新增的数据未保存,是否保存?"
                                , titel
                                , MessageBoxButtons.YesNoCancel
                                , MessageBoxIcon.Question);
            return result;
        }

        public static DialogResult ShowPartInDialog(Control control, string title)
        {
            DevExpress.XtraEditors.XtraForm form = new DevExpress.XtraEditors.XtraForm();
            form.Size = new Size(control.Size.Width + 5, control.Size.Height + 15);
            form.StartPosition = FormStartPosition.CenterParent;
            form.FormBorderStyle = FormBorderStyle.FixedSingle;
            form.MaximizeBox = false;
            form.Text = title;
            form.Controls.Add(control);
            control.Dock = DockStyle.Fill;
            return form.ShowDialog();
        }

        #endregion

        public static string GetFileTypeByFileName(string fileName)
        {
            return Path.GetExtension(fileName);
        }

        public static string GetFileSizeString(long length)
        {
            string fileSize = string.Empty;

            if (length >= 1000000000)
                fileSize = (length / 1000000000m).ToString("F2") + " MB";
            else if (length >= 1000000)
                fileSize = (length / 1000000m).ToString("F2") + " MB";
            else if (length >= 1000)
                fileSize = (length / 1000m).ToString("F2") + " KB";
            else
                fileSize = length.ToString() + " B";

            return fileSize;
        }

        public static void SetUnReadStyle(AppearanceObject a)
        {
            a.Options.UseFont = true;
            a.Font = new Font(a.Font, FontStyle.Bold);
        }

        public static void XtraRichEditLoadByte(DevExpress.XtraRichEdit.RichEditControl reControl, byte[] Content)
        {
            if (Content == null) return;

            MemoryStream ms = new MemoryStream(Content);

            string textconent = System.Text.UnicodeEncoding.GetEncoding("GB2312").GetString(Content);
            if (textconent.Contains("</"))
            {
                reControl.LoadDocument(ms, DevExpress.XtraRichEdit.DocumentFormat.Html);
            }
            else
            {
                reControl.LoadDocument(ms, DevExpress.XtraRichEdit.DocumentFormat.PlainText);
            }

            ms.Close();
        }

        public static byte[] XtraRichEditGetHtmlByte(DevExpress.XtraRichEdit.RichEditControl reControl)
        {
            MemoryStream ms = new MemoryStream();
            reControl.SaveDocument(ms, DevExpress.XtraRichEdit.DocumentFormat.Html);
            return ms.GetBuffer();
        }


        public static byte[] XtraRichEditGetTextByte(DevExpress.XtraRichEdit.RichEditControl reControl)
        {
            return System.Text.UnicodeEncoding.GetEncoding("GB2312").GetBytes(reControl.Text);
            //MemoryStream ms = new MemoryStream();
            //reControl.SaveDocument(ms, DevExpress.XtraRichEdit.DocumentFormat.PlainText);
            //return ms.GetBuffer();

        }

        #region UIHelper

        /// <summary>
        /// 绑定controls的keyDown的Enter事件到执行btnSearch的PerformClick方法
        /// </summary>
        public static void SearchPartKeyEnterToSearch(List<Control> controls, DevExpress.XtraEditors.SimpleButton btnSearch)
        {
            if (controls == null || controls.Count == 0) return;
            foreach (var item in controls)
            {
                item.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.Enter) btnSearch.PerformClick();
                };
            }
        }

        #endregion
        public static ICP.Message.ServiceInterface.Message InitMessage()
        {
            ICP.Message.ServiceInterface.Message entry = new ICP.Message.ServiceInterface.Message();
            entry.Id = Guid.NewGuid();
            entry.CreateBy = LocalData.UserInfo.LoginID;
            entry.CreatorName = LocalData.IsEnglish ? LocalData.UserInfo.LoginName : LocalData.UserInfo.UserName;
            entry.CreateDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
            entry.Type = MessageType.Fax;
            return entry;

        }
    }

    /// <summary>
    /// 提供从操作系统读取图标的方法
    /// </summary>
    public class GetSystemIcon
    {
        private static Dictionary<string, Icon> icons = new Dictionary<string, Icon>();
        /// <summary>
        /// 依据文件名读取图标，若指定文件不存在，则返回空值。
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static Icon GetIconByFileName(string fileName)
        {   

            if (fileName == null || fileName.Equals(string.Empty)) return null;
            if (!File.Exists(fileName)) return null;

            SHFILEINFO shinfo = new SHFILEINFO();
            //Use this to get the small Icon
            IconHelper.SHGetFileInfo(fileName, 0, ref shinfo,
                  (uint)Marshal.SizeOf(shinfo), IconHelper.SHGFI_ICON | IconHelper.SHGFI_SMALLICON);
            //The icon is returned in the hIcon member of the shinfo struct
            System.Drawing.Icon myIcon = System.Drawing.Icon.FromHandle(shinfo.hIcon);
            return myIcon;
        }

        /// <summary>
        /// 给出文件扩展名（.*），返回相应图标
        /// 若不以"."开头则返回文件夹的图标。
        /// </summary>
        /// <param name="fileType"></param>
        /// <param name="isLarge"></param>
        /// <returns></returns>
        public static Icon GetIconByFileType(string fileType, bool isLarge)
        {
            if (fileType == null || fileType.Equals(string.Empty)) return null;
            if (icons.ContainsKey(fileType))
            { 
              return icons[fileType];
            }
            RegistryKey regVersion = null;
            string regFileType = null;
            string regIconString = null;
            string systemDirectory = Environment.SystemDirectory + "\\";

            if (fileType[0] == '.')
            {
                //读系统注册表中文件类型信息
                regVersion = Registry.ClassesRoot.OpenSubKey(fileType, true);
                if (regVersion != null)
                {
                    regFileType = regVersion.GetValue("") as string;
                    regVersion.Close();
                    regVersion = Registry.ClassesRoot.OpenSubKey(regFileType + @"\DefaultIcon", true);
                    if (regVersion != null)
                    {
                        regIconString = regVersion.GetValue("") as string;
                        regVersion.Close();
                    }
                }
                if (regIconString == null)
                {
                    //没有读取到文件类型注册信息，指定为未知文件类型的图标
                    regIconString = systemDirectory + "shell32.dll,0";
                }
            }
            else
            {
                //直接指定为文件夹图标
                regIconString = systemDirectory + "shell32.dll,3";
            }
            string[] fileIcon = regIconString.Split(new char[] { ',' });
            if (fileIcon.Length != 2)
            {
                //系统注册表中注册的标图不能直接提取，则返回可执行文件的通用图标
                fileIcon = new string[] { systemDirectory + "shell32.dll", "2" };
            }
            Icon resultIcon = null;
            try
            {
                //调用API方法读取图标
                int[] phiconLarge = new int[1];
                int[] phiconSmall = new int[1];
                uint count = IconHelper.ExtractIconEx(fileIcon[0], Int32.Parse(fileIcon[1]), phiconLarge, phiconSmall, 1);
                IntPtr IconHnd = new IntPtr(isLarge ? phiconLarge[0] : phiconSmall[0]);
                resultIcon = Icon.FromHandle(IconHnd);
            }
            catch { }
            icons.Add(fileType, resultIcon);
            return resultIcon;
        }

        #region IconHelper

        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public IntPtr iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        };
        class IconHelper
        {
            public const uint SHGFI_ICON = 0x100;
            public const uint SHGFI_LARGEICON = 0x0; // 'Large icon
            public const uint SHGFI_SMALLICON = 0x1; // 'Small icon
            [DllImport("shell32.dll")]
            public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);
            [DllImport("shell32.dll")]
            public static extern uint ExtractIconEx(string lpszFile, int nIconIndex, int[] phiconLarge, int[] phiconSmall, uint nIcons);

            [DllImport("User32.dll", EntryPoint = "DestroyIcon")]
            public static extern int DestroyIcon(IntPtr hIcon);

        }

        #endregion
    }

    public class FolderDialog : FolderNameEditor
    {
        string _title = string.Empty;
        FolderNameEditor.FolderBrowser fDialog = new System.Windows.Forms.Design.FolderNameEditor.FolderBrowser();
        public FolderDialog() { }

        public DialogResult DisplayDialog()
        {
            return DisplayDialog("请选择一个文件夹");
        }

        public DialogResult DisplayDialog(string description)
        {
            fDialog.StartLocation = FolderBrowserFolder.Desktop;
            fDialog.Description = description;
            return fDialog.ShowDialog();
        }
        public string Path
        {
            get { return fDialog.DirectoryPath; }
        }

        ~FolderDialog() { fDialog.Dispose(); }
    }

    public class UIConnectionHelper
    {
        public delegate bool SaveDataDelegate();

        public static void ParentChangingForEditPart(IListPart listPart, SaveDataDelegate saveData, BaseDataObject childDataSource, CancelEventArgs e, string partName)
        {
            if (childDataSource == null) return;

            if (childDataSource.IsNew)
            {
                DialogResult dlg = Utility.EnquireIsSaveCurrentDataByNew(partName);
                if (dlg == DialogResult.Yes) e.Cancel = !saveData();
                else if (dlg == DialogResult.Cancel) e.Cancel = true;
                else if (dlg == DialogResult.No && childDataSource.IsNew)
                { listPart.RemoveItem(listPart.Current); }
            }
            else if (childDataSource.IsDirty)
            {
                DialogResult dlg = Utility.EnquireIsSaveCurrentDataByUpdated(partName);
                if (dlg == DialogResult.Yes) e.Cancel = !saveData();
                else if (dlg == DialogResult.Cancel) e.Cancel = true;
                else if (dlg == DialogResult.No && childDataSource.IsNew)
                { listPart.RemoveItem(listPart.Current); }
            }
        }

        public static void ParentChangingForListPart<T>(SaveDataDelegate saveData, List<T> childDataSource, CancelEventArgs e, string partName) where T : BaseDataObject
        {
            if (childDataSource == null || childDataSource.Count == 0) return;

            bool isNew = false, isDirty = false;
            foreach (var item in childDataSource)
            {
                if (item.IsNew) { isNew = true; break; }
                if (item.IsDirty) { isDirty = true; break; }
            }

            if (isNew)
            {
                DialogResult dlg = Utility.EnquireIsSaveCurrentDataByNew(partName);
                if (dlg == DialogResult.Yes) e.Cancel = !saveData();
                else if (dlg == DialogResult.Cancel) e.Cancel = true;
                else if (dlg == DialogResult.No) { return; }
            }
            else if (isDirty)
            {
                DialogResult dlg = Utility.EnquireIsSaveCurrentDataByUpdated(partName);
                if (dlg == DialogResult.Yes) e.Cancel = !saveData();
                else if (dlg == DialogResult.Cancel) e.Cancel = true;
                else if (dlg == DialogResult.No) { return; }
            }
        }

        public static void ParentChangingForBaseListPart<T>(SaveDataDelegate saveData, BaseList<T> childDataSource, CancelEventArgs e, string partName) where T : BaseDataObject
        {
            if (childDataSource.IsDirty)
            {
                DialogResult dlg = Utility.EnquireIsSaveCurrentDataByUpdated(partName);
                if (dlg == DialogResult.Yes) e.Cancel = !saveData();
                else if (dlg == DialogResult.Cancel) e.Cancel = true;
                else if (dlg == DialogResult.No) { return; }
            }
        }

    }

    /// <summary>
    /// 继承自IComparer
    /// </summary>
    public class ListViewColumnSorter : IComparer
    {
        /// <summary>
        /// 指定按照哪个列排序
        /// </summary>
        private int ColumnToSort;
        /// <summary>
        /// 指定排序的方式
        /// </summary>
        private SortOrder OrderOfSort;
        /// <summary>
        /// 声明CaseInsensitiveComparer类对象，
        /// 参见ms-help://MS.VSCC.2003/MS.MSDNQTR.2003FEB.2052/cpref/html/frlrfSystemCollectionsCaseInsensitiveComparerClassTopic.htm
        /// </summary>
        private CaseInsensitiveComparer ObjectCompare;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ListViewColumnSorter()
        {
            // 默认按第一列排序
            ColumnToSort = 0;

            // 排序方式为不排序
            OrderOfSort = SortOrder.None;

            // 初始化CaseInsensitiveComparer类对象
            ObjectCompare = new CaseInsensitiveComparer();
        }

        /// <summary>
        /// 重写IComparer接口.
        /// </summary>
        /// <param name="x">要比较的第一个对象</param>
        /// <param name="y">要比较的第二个对象</param>
        /// <returns>比较的结果.如果相等返回0，如果x大于y返回1，如果x小于y返回-1</returns>
        public int Compare(object x, object y)
        {
            int compareResult;
            ListViewItem listviewX, listviewY;

            // 将比较对象转换为ListViewItem对象
            listviewX = (ListViewItem)x;
            listviewY = (ListViewItem)y;

            // 比较
            compareResult = ObjectCompare.Compare(listviewX.SubItems[ColumnToSort].Text, listviewY.SubItems[ColumnToSort].Text);

            // 根据上面的比较结果返回正确的比较结果
            if (OrderOfSort == SortOrder.Ascending)
            {
                // 因为是正序排序，所以直接返回结果
                return compareResult;
            }
            else if (OrderOfSort == SortOrder.Descending)
            {
                // 如果是反序排序，所以要取负值再返回
                return (-compareResult);
            }
            else
            {
                // 如果相等返回0
                return 0;
            }
        }

        /// <summary>
        /// 获取或设置按照哪一列排序.
        /// </summary>
        public int SortColumn
        {
            set
            {
                ColumnToSort = value;
            }
            get
            {
                return ColumnToSort;
            }
        }

        /// <summary>
        /// 获取或设置排序方式.
        /// </summary>
        public SortOrder Order
        {
            set
            {
                OrderOfSort = value;
            }
            get
            {
                return OrderOfSort;
            }
        }

    }

   

}

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.ClientComponents.UIFramework;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;

namespace ICP.FCM.Common.UI
{
    public class Utility
    {   
        /// <summary>
        /// 根据Key从集合中获取值
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static object GetValue(string key,IDictionary<string, object> values)
        {
            if (values == null)
                return null;
            if (values.ContainsKey(key))
            {
                return values[key];
            }
            return null;
        }
        
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

        #region FinderHelper

        /*根据信息转换为object[]*/
        public static object[] GetSingleSearchResult<T>(T data, string[] returnFields)
        {
            object[] result = new object[returnFields.Length];
            for (int i = 0; i < returnFields.Length; i++)
            {
                result[i] = Utility.GetObjectPropertyValue<T>(data, returnFields[i]);
            }

            return result;
        }

        /*根据信息转换为object[]*/
        public static object[] GetMultiSearchResult<T>(List<T> datas, string[] returnFields)
        {
            object[] result = new object[datas.Count];
            for (int i = 0; i < datas.Count; i++)
            {
                result[i] = Utility.GetSingleSearchResult<T>(datas[i], returnFields);
            }
            return result;
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
            string info = string.Empty;

            Type type = typeof(T);

            PropertyInfo[] propertys = type.GetProperties();

            foreach (PropertyInfo property in propertys)
            {
                if (property.PropertyType.IsValueType || property.PropertyType.Name == "String")
                {
                    if (property.PropertyType.FullName.Contains("Guid")) continue;

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

        #region ShowDialog

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
        /// 弹出一个是提示"数据有更改,是否保存数据"的对话框
        /// </summary>
        /// <returns></returns>
        public static DialogResult EnquireIsSaveCurrentDataByUpdated()
        {
            DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "The data was changed,Sure save?" : "数据有更改是否保存?"
                                , LocalData.IsEnglish ? "Tip" : "提示"
                                , MessageBoxButtons.YesNoCancel
                                , MessageBoxIcon.Question);
            return result;
        }

        /// <summary>
        /// 功能描述：打开一个模态窗口,返回该模态窗口的DialogReasult的值
        /// </summary>
        /// <param name="control"></param>
        /// <param name="title"></param>
        public static DialogResult ShowDialog(System.Windows.Forms.Control control, string title)
        {
            return Utility.ShowDialog(control, title, System.Windows.Forms.FormBorderStyle.FixedSingle);
        }

        /// <summary>
        /// 功能描述：打开一个模态窗口,返回该模态窗口的DialogReasult的值
        /// </summary>
        /// <param name="control"></param>
        /// <param name="title"></param>
        public static DialogResult ShowDialog(System.Windows.Forms.Control control, string title, System.Windows.Forms.FormBorderStyle formBorderStyle)
        {
            DevExpress.XtraEditors.XtraForm form = new DevExpress.XtraEditors.XtraForm();
            form.MaximizeBox = false;
            form.MinimizeBox = false;
            form.Text = title;
            form.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            if (control.Height >= System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height)
            {
                form.Size = new System.Drawing.Size(control.Size.Width + 10, control.Size.Height + 30);
            }
            else
            {
                form.Size = new System.Drawing.Size(control.Size.Width, control.Size.Height + 30);
            }
            form.FormBorderStyle = formBorderStyle;
            control.Dock = DockStyle.Fill;
            form.Controls.Add(control);
            return form.ShowDialog();
        }

        #endregion

        #region ShowEditPart

        public delegate void EditPartSaved(object[] prams);
        /// <summary>
        /// 显示一个编辑页面到MainWorkspace
        /// </summary>
        /// <typeparam name="T">编辑页面类型</typeparam>
        /// <param name="Workitem">WorkItem</param>
        /// <param name="source">数据源</param>
        /// <param name="title">Title</param>
        /// <param name="editPartSaved">编辑页面保存后执行的委托,可以传Null</param>
        public static void ShowEditPart<T>(WorkItem Workitem, object source, string title, EditPartSaved editPartSaved) where T : BaseEditPart
        {
            Utility.ShowEditPart<T>(Workitem, source, null, title, editPartSaved);
        }

        /// <summary>
        /// 显示一个编辑页面到MainWorkspace
        /// </summary>
        /// <typeparam name="T">编辑页面类型</typeparam>
        /// <param name="Workitem">WorkItem</param>
        /// <param name="source">数据源</param>
        /// <param name="title">Title</param>
        /// <param name="editPartSaved">编辑页面保存后执行的委托,可以传Null</param>
        public static void ShowEditPart<T>(WorkItem Workitem, object source, IDictionary<string, object> values, string title, EditPartSaved editPartSaved) where T : BaseEditPart
        {


            T editPart = Workitem.Items.AddNew<T>();
            if (values != null) editPart.Init(values);
            editPart.DataSource = (source);

            IWorkspace mainWorkspace = Workitem.Workspaces[ICP.Framework.CommonLibrary.Client.ClientConstants.MainWorkspace];
            SmartPartInfo smartPartInfo = new SmartPartInfo();
            smartPartInfo.Title = title;
            mainWorkspace.Show(editPart, smartPartInfo);

            if (editPartSaved != null)
            {
                editPart.Saved += delegate(object[] prams)
                {
                    editPartSaved(prams);
                };
            }
        }

        #endregion
    }

    public class UIModelHelper
    {
        public static T GetNormalObject<T>() where T : new()
        {
            T t = new T();

            Type sourceType = t.GetType();
            PropertyInfo[] properties = sourceType.GetProperties();
            foreach (var property in properties)
            {
                PropertyInfo tp = sourceType.GetProperty(property.Name);
                if (property.PropertyType == typeof(string))
                {
                    property.SetValue(t, tp.Name, null);
                }
                else if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
                {
                    property.SetValue(t, DateTime.Now, null);
                }
                else if (property.PropertyType == typeof(int) || property.PropertyType == typeof(int?))
                {
                    property.SetValue(t, DateTime.Now.Year, null);
                }
                else if (property.PropertyType == typeof(decimal) || property.PropertyType == typeof(decimal?))
                {
                    property.SetValue(t, DateTime.Now.Year, null);
                }
                else if (property.PropertyType == typeof(short) || property.PropertyType == typeof(short?))
                {
                    property.SetValue(t, (short)1, null);
                }
                else if (property.PropertyType == typeof(Guid) || property.PropertyType == typeof(Guid?))
                {
                    property.SetValue(t, Guid.NewGuid(), null);
                }
                else if (property.PropertyType.IsEnum)
                {
                    property.SetValue(t, (short)1, null);
                }

            }

            return t;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using System.Reflection;
using ICP.Framework.ClientComponents.UIFramework;
using System.ComponentModel;

namespace ICP.Sys.UI
{
    public class FunctionConstants 
    {
        public const string SYSTEM_ROLELIST = "SYSTEM_ROLELIST";
        public const string SYSTEM_JOBLIST = "SYSTEM_JOBLIST";
        public const string SYSTEM_USERLIST = "SYSTEM_USERLIST";
        public const string SYSTEM_ORGANIZATIONLIST = "SYSTEM_ORGANIZATIONLIST";
        public const string SYSTEM_PERMISSIONLIST = "SYSTEM_PERMISSIONLIST";
        public const string SYSTEM_UICONFIGURELIST = "SYSTEM_UICONFIGURELIST";

        public const string SYSTEM_NEWFEEDBACK = "SYSTEM_NEWFEEDBACK";
        public const string SYSTEM_MYFEEDBACKS = "SYSTEM_MYFEEDBACKS";
        public const string SYSTEM_SAMPLENEWFEEDBACK = "SYSTEM_SAMPLENEWFEEDBACK";
        public const string SYSTEM_HELPDOCUMENT = "SYSTEM_HELPDOCUMENT";
        public const string SYSTEM_SYSTEMERRORLOG = "SYSTEM_SYSTEMERRORLOG";

        public const string SYSTEM_WORKSPACEVIEW = "SYSTEM_WORKSPACEVIEW";
    }

    public class Comm_Constants
    {
        public const string Common_FinderConfirm = "Common_FinderConfirm";
    }

    public class SearchFieldConstants
    {
        public static readonly string[] ResultValue = new string[] { "ID", "Code", "EName", "CName" };


        public const string VesselVoyage = @"Vessel/Voyage";
        public const string Vessel = @"Vessel";
        public const string Voyage = @"Voyage";

        public const string CodeName = @"Code/Name";
        public const string Name = "Name";
        public const string Code = "Code";
    }

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
            //w.Settings.Encoding = System.Text.UnicodeEncoding.Unicode;
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
        /// 把source对象的值拷贝到一个类型为targetType的targe对象
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
        public static string GetObjectPropertyStringValue<T>(T t, string propertyname)
        {
            object o = Utility.GetObjectPropertyValue<T>(t, propertyname);
            if (o == null) return string.Empty;
            else return o.ToString();
        }

        /// <summary>
        /// 获取对象某一属性值
        /// </summary>
        public static object GetObjectPropertyValue<T>(T t, string propertyname)
        {
            Type type = typeof(T);

            PropertyInfo property = type.GetProperty(propertyname);

            if (property == null) return string.Empty;

            object o = property.GetValue(t, null);
            return o;
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

        public static bool GuidIsNullOrEmpty(Guid? id)
        {
            if (id == null || id == Guid.Empty) return true;
            else return false;
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

        public static DialogResult ShowPartInDialog(Control control,string title)
        {
            DevExpress.XtraEditors.XtraForm form = new DevExpress.XtraEditors.XtraForm();
            form.Size = new Size(control.Size.Width+5, control.Size.Height+15);
            form.StartPosition = FormStartPosition.CenterParent;
            form.FormBorderStyle = FormBorderStyle.FixedSingle;
            form.MaximizeBox = false;
            form.Text = title;
            form.Controls.Add(control);
            control.Dock = DockStyle.Fill;
            return form.ShowDialog();
        }

        #endregion

        #region UI
        /// <summary>
        /// 移除绑定controls的keyDown的Enter事件到执行button的PerformClick方法
        /// </summary>
        /// <param name="controls"></param>
        /// <param name="handler"></param>
        public static void RemoveSetControlKeyEnterToClickButton(List<Control> controls, KeyEventHandler handler)
        {
            if (controls == null || controls.Count == 0) return;
            foreach (var item in controls)
            {
                item.KeyDown -= handler;

            }
        }
        /// <summary>
        /// 绑定controls的keyDown的Enter事件到执行button的PerformClick方法
        /// </summary>
        public static void SetControlKeyEnterToClickButton(List<Control> controls, DevExpress.XtraEditors.SimpleButton button,KeyEventHandler handler)
        {
            if (controls == null || controls.Count == 0) return;
            foreach (var item in controls)
            {
                item.KeyDown += handler;
             
            }
        }

        public static Form DockPartInForm(UserControl part)
        {
            Form form = new Form();
            form.BackColor = part.BackColor;
            form.Size = new System.Drawing.Size(part.Width + 5, part.Height + 15);
            part.Dock = DockStyle.Fill;
            form.Controls.Add(part);
            return form;
        }
        #endregion

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

        #region Grid Helper
        /// <summary>
        /// 显示Grid行号
        /// </summary>
        public static void ShowGridRowNo(DevExpress.XtraGrid.Views.Grid.GridView gridView)
        {
            gridView.IndicatorWidth = 40;
            gridView.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(gridView_CustomDrawRowIndicator);
        }

        static void gridView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1) % 1000);
            }
        }

        public static void SetGridViewClickIndicatorHeader2SelectAll(DevExpress.XtraGrid.Views.Grid.GridView gv)
        {
            gv.MouseDown += delegate(object sender, MouseEventArgs e)
            {

                DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hitInfo = gv.CalcHitInfo(e.Location);
                if (hitInfo != null
                    && hitInfo.RowHandle < 0
                    && hitInfo.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.ColumnButton
                    && hitInfo.InColumn == false
                    && hitInfo.InColumnPanel == false
                    && hitInfo.InFilterPanel == false
                    && hitInfo.InGroupColumn == false
                    && hitInfo.InGroupPanel == false
                    && hitInfo.InRow == false
                    && hitInfo.InRowCell == false
                    )
                {
                    gv.SelectAll();
                }

            };
        }

        #endregion


        #region Tree Helper
        public static void ShowTreeRowNo(DevExpress.XtraTreeList.TreeList treeList)
        {
            treeList.IndicatorWidth = 40;
            treeList.CustomDrawNodeIndicator += new DevExpress.XtraTreeList.CustomDrawNodeIndicatorEventHandler(treeList_CustomDrawNodeIndicator);

        }

        static void treeList_CustomDrawNodeIndicator(object sender, DevExpress.XtraTreeList.CustomDrawNodeIndicatorEventArgs e)
        {
            DevExpress.XtraTreeList.TreeList tmpTree = sender as DevExpress.XtraTreeList.TreeList;
            DevExpress.Utils.Drawing.IndicatorObjectInfoArgs args = e.ObjectArgs as DevExpress.Utils.Drawing.IndicatorObjectInfoArgs;
            if (args != null)
            {
                int rowNum = tmpTree.GetVisibleIndexByNode(e.Node) + 1;
                args.DisplayText = rowNum.ToString();
            }
            e.ImageIndex = -1; 
        }
        #endregion


    }

    public class UIConnectionHelper
    {
        public delegate bool SaveDataDelegate();

        public static void ParentChangingForEditPart(IListPart listPart, SaveDataDelegate saveData, BaseDataObject childDataSource, CancelEventArgs e, string partName)
        {
            ParentChangingForEditPart(listPart, saveData, childDataSource, e, partName, true);
        }

        public static void ParentChangingForEditPart(IListPart listPart, SaveDataDelegate saveData, BaseDataObject childDataSource, CancelEventArgs e, string partName,bool removeMainList)
        {
            if (childDataSource == null) return;

            if (childDataSource.IsNew)
            {
                DialogResult dlg = Utility.EnquireIsSaveCurrentDataByNew(partName);
                if (dlg == DialogResult.Yes) e.Cancel = !saveData();
                else if (dlg == DialogResult.Cancel) e.Cancel = true;
                else if (dlg == DialogResult.No && childDataSource.IsNew && removeMainList)
                { listPart.RemoveItem(listPart.Current); }
            }
            else if (childDataSource.IsDirty)
            {
                DialogResult dlg = Utility.EnquireIsSaveCurrentDataByUpdated(partName);
                if (dlg == DialogResult.Yes) e.Cancel = !saveData();
                else if (dlg == DialogResult.Cancel) e.Cancel = true;
                else if (dlg == DialogResult.No && childDataSource.IsNew && removeMainList)
                { listPart.RemoveItem(listPart.Current); }
            }
        }

        public static void ParentChangingForListPart<T>(SaveDataDelegate saveData,List<T> childDataSource, CancelEventArgs e,string partName) where T:BaseDataObject
        {
            if (childDataSource == null || childDataSource.Count == 0) return;

            bool isNew = false,isDirty =false;
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

    public enum ViewStyle
    { 
        All,
        Selected,
        UnSelected
    }

}



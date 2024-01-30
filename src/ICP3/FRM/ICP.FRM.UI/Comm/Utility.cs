using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FRM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;
using System.Drawing;
using ICP.Framework.CommonLibrary.Common;
using System.Data;
using DevExpress.XtraEditors.Repository;
using ICP.FRM.UI.SearchRate;
using ICP.Framework.ClientComponents;

namespace ICP.FRM.UI
{
    public static class Utility
    {  
        /// <summary>
        /// 使用指定分隔符从字符串中获取不重复的Guid数组
        /// </summary>
        /// <param name="initStr"></param>
        /// <param name="seperator"></param>
        /// <returns></returns>
        public static Guid[] GetIds(string initStr, char seperator)
        {
            if (string.IsNullOrEmpty(initStr))
                return new Guid[] {Guid.Empty };
            string[] list = initStr.Split(seperator);
            List<Guid> idsList = new List<Guid>();

            foreach (string str in list)
            {
                if (!string.IsNullOrEmpty(str) && !idsList.Contains(new Guid(str)))
                {
                    idsList.Add(new Guid(str));
                }
            }

            return idsList.ToArray();
        }
        /// <summary>
        /// 使用';'分隔符从字符串中获取不重复的Guid数组
        /// </summary>
        /// <param name="initStr"></param>
        /// <returns></returns>
        public static Guid[] GetIds(string initStr)
        { 
           return GetIds(initStr,';');
        }
        #region 处理对象

        /// <summary>
        /// 深拷贝,通过序列化对象再反序列化得出新的对象
        /// </summary>
        public static T Clone<T>(T t)
        {
            T clone;
            XDocument doc = new XDocument();

            XmlWriter w = doc.CreateWriter();

            XmlSerializer s = new XmlSerializer(typeof(T));
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
                    && p.PropertyType.GetInterface(typeof(IList).FullName) != null)//如果是泛集合
                {
                    //不处理

                    object pValue = p.GetValue(source, null);
                    foreach (object obj in (pValue as IEnumerable))
                    {
                        //如果未实例化集合属性则实例化它
                        if (tp.GetValue(targe, null) == null) tp.SetValue(targe, Activator.CreateInstance(tp.PropertyType), null);
                        //获取Item属性的类型
                        string subitemTypeName = Regex.Match(tp.PropertyType.FullName, @"\[\[(?<val>.*?)\]\]").Groups["val"].Value;
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
                        IList temTarget = tp.GetValue(targe, null) as IList;
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
        /// <param name="targe">需要拷贝到的对象(必须是targetType类型)</param>
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
                    && p.PropertyType.GetInterface(typeof(IList).FullName) != null)//如果是泛集合
                {
                    //不处理


                    try
                    {
                        object pValue = p.GetValue(source, null);
                        foreach (object obj in (pValue as IEnumerable))
                        {
                            //如果未实例化集合属性则实例化它
                            if (tp.GetValue(targe, null) == null) tp.SetValue(targe, Activator.CreateInstance(tp.PropertyType), null);
                            //获取Item属性的类型
                            string subitemTypeName = Regex.Match(tp.PropertyType.FullName, @"\[\[(?<val>.*?)\]\]").Groups["val"].Value;
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
                            IList temTarget = tp.GetValue(targe, null) as IList;
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

        /// <summary>
        /// 克隆DataRow,tager的列数必需和source的列数相同,否则会报错
        /// </summary>
        /// <param name="source">源</param>
        /// <param name="tager">目标</param>
        public static void CloneDataRow(DataRow source, DataRow tager)
        {
            for (int i = 0; i < source.ItemArray.Length; i++)
			{
			    tager[i]= source[i];
			}
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

        #region UIHelper

        /// <summary>
        /// 绑定controls的keyDown的Enter事件到执行btnSearch的PerformClick方法
        /// </summary>
        public static void SearchPartKeyEnterToSearch(List<Control> controls, SimpleButton btnSearch)
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


        /// <summary>
        /// 绑定controls的keyDown的F2事件到执行btnSearch的PerformClick方法
        /// </summary>
        public static void SearchPartKeyF2ToSearch(List<Control> controls, SimpleButton btn)
        {
            if (controls == null || controls.Count == 0) return;
            foreach (var item in controls)
            {
                item.KeyDown += delegate(object sender, KeyEventArgs e)
                {
                    if (e.KeyCode == Keys.F2) btn.PerformClick();
                };
            }
        }

        /// <summary>
        /// SetGridViewColumnAllowEditColor
        /// </summary>
        /// <param name="gvMain">GridView</param>
        public static void SetGridViewColumnAllowEditColor(GridView gvMain)
        {
            foreach (GridColumn item in gvMain.Columns)
            {
                if (item.OptionsColumn.AllowEdit == false)
                {
                    item.AppearanceCell.ForeColor = SystemColors.ControlDark;
                    item.AppearanceCell.Options.UseForeColor = true;
                }
            }
        }

        #endregion

        #region Validate

        public static string ValidateCommodityString(string commodityString, List<CommodityList> commoditys)
        {
            if (string.IsNullOrEmpty(commodityString)) return string.Empty;
            if (commoditys == null || commoditys.Count == 0) return string.Empty;

            string errorMessage = string.Empty;

            string strExcept = "[EXCEPT:";
            string strTager = string.Empty;
            if (commodityString.Contains(strExcept))
            {
                int index = commodityString.IndexOf(strExcept);

                string strExceptTager = commodityString.Substring(index, commodityString.Length - index);
                int intLastIndexOf = strExceptTager.LastIndexOf("]");
                if (intLastIndexOf < 0 || intLastIndexOf != strExceptTager.Length - 1) return "EXCEPT Info Error.";

                strExceptTager = strExceptTager.Replace(strExcept, string.Empty);
                strExceptTager = strExceptTager.Substring(0, strExceptTager.Length - 1);

                strTager = commodityString.Substring(0, index);
                strTager += strExceptTager;
            }
            else
                strTager = commodityString;

            //ICP.Framework.CommonLibrary.GlobalConstants.DividedSymbol
            string[] commodityArray = strTager.Split(UIConstants.DividedSymbol);
            if (commodityString.Length != 0)
            {
                List<string> commodityStrs = new List<string>();
                foreach (var item in commoditys)
                    commodityStrs.Add(item.EName);

                foreach (var item in commodityArray)
                {
                    if (commodityStrs.Contains(item) == false)
                    {
                        if (string.IsNullOrEmpty(errorMessage) == false)
                            errorMessage += GlobalConstants.DividedSymbol;

                        errorMessage += item;
                    }
                }                
            }

            return errorMessage;
        }

        #endregion

        #region NormalValue

        public static void SetNormalRateUnitValue(OceanInfo data)
        {
            if (data.OceanUnits != null && data.OceanUnits.Count > 0) return;
            data.OceanUnits = new List<OceanUnitList>();

            OceanUnitList unit1 = new OceanUnitList();
            unit1.UnitID = new Guid("121A7E2F-A909-E011-916B-001321CC6D9F");
            unit1.UnitName = string.Empty;
            data.OceanUnits.Add(unit1);

            OceanUnitList unit2 = new OceanUnitList();
            unit2.UnitID = new Guid("00A36C93-8AB9-DF11-BA3B-001321CC6D9F");
            unit2.UnitName = string.Empty;
            data.OceanUnits.Add(unit2);

            OceanUnitList unit3 = new OceanUnitList();
            unit3.UnitID = new Guid("CCB42EAF-A4C0-DF11-B515-0014C25F9005");
            unit3.UnitName = string.Empty;
            data.OceanUnits.Add(unit3);

            OceanUnitList unit4 = new OceanUnitList();
            unit4.UnitID = new Guid("9C8CE9A6-A4C0-DF11-B515-0014C25F9005");
            unit4.UnitName = string.Empty;
            data.OceanUnits.Add(unit4);

            OceanUnitList unit5 = new OceanUnitList();
            unit5.UnitID = new Guid("E65F27A5-ABA6-DF11-9F52-001321CC6D9F");
            unit5.UnitName = string.Empty;
            data.OceanUnits.Add(unit5);

            OceanUnitList unit6 = new OceanUnitList();
            unit6.UnitID = new Guid("FE6F9F43-5EBA-DF11-BA3B-001321CC6D9F");
            unit6.UnitName = string.Empty;
            data.OceanUnits.Add(unit6);
        }

        #endregion

        #region ExecuteOnec

        public delegate void SetDelegateSource();
        
        /// <summary>
        /// 当控件的Enter事件触发就执行method(只执行一次)
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="method">要执行的方法</param>
        public static void SetEnterToExecuteOnec(Control control, SetDelegateSource method)
        {
            bool isInit = false;
            control.Enter += delegate
            {
                if (isInit) return;
                isInit = true;
                method();
            };
        }

        /// <summary>
        /// 当控件的Enter事件触发就执行method(只执行一次)
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="method">要执行的方法</param>
        public static void SetEnterToExecuteOnec(RepositoryItemImageComboBox control, SetDelegateSource method)
        {
            bool isInit = false;
            control.Enter += delegate
            {
                if (isInit) return;
                isInit = true;
                method();
            };
        }

        /// <summary>
        /// 当控件的Enter事件触发就执行method(只执行一次)
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="method">要执行的方法</param>
        public static void SetEnterToExecuteOnec(List<Control> controls, SetDelegateSource method)
        {
            bool isInit = false;

            foreach (var item in controls)
            {
                item.Enter += delegate
                {
                    if (isInit) return;
                    isInit = true;
                    method();
                };
            }
        }

        #endregion

        #region Grid Helper
        /// <summary>
        /// 显示Grid行号
        /// </summary>
        public static void ShowGridRowNo(GridView gridView)
        {
            gridView.IndicatorWidth = 40;
            gridView.CustomDrawRowIndicator += new RowIndicatorCustomDrawEventHandler(gridView_CustomDrawRowIndicator);
        }

        static void gridView_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)
            {
                e.Info.DisplayText = Convert.ToString((e.RowHandle + 1));
            }
        }

        public static void SetGridViewClickIndicatorHeader2SelectAll(GridView gv)
        {
            gv.MouseDown += delegate(object sender, MouseEventArgs e)
            {

                GridHitInfo hitInfo = gv.CalcHitInfo(e.Location);
                if (hitInfo != null
                    && hitInfo.RowHandle < 0
                    && hitInfo.HitTest == GridHitTest.ColumnButton
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

        #region ShowDialog


        /// <summary>
        /// 弹出一个是提示" 删除此项会删除此项的所有子项,是否删除"的对话框
        /// </summary>
        /// <returns></returns>
        public static bool EnquireIsDeleteCurrentDataByHasChild()
        {


            DialogResult result = XtraMessageBox.Show(LocalData.IsEnglish ? "Delete Current Data Will Delete Current Data All Child.,Srue Delete?" : "删除此项会删除此项的所有子项,是否删除?",
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
            return EnquireIsSaveCurrentDataByUpdated(null);
        }

        /// <summary>
        /// 弹出一个是提示"数据有更改,是否保存数据"的对话框
        /// </summary>
        /// <returns></returns>
        public static DialogResult EnquireIsSaveCurrentDataByUpdated(string titel)
        {
            if (string.IsNullOrEmpty(titel)) titel = LocalData.IsEnglish ? "Tip" : "提示";

            DialogResult result = XtraMessageBox.Show(LocalData.IsEnglish ? "The data was changed,Sure save?" : "数据有更改是否保存?"
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
            return EnquireIsSaveCurrentDataByNew(null);
        }

        /// <summary>
        /// 弹出一个是提示"新增的数据未保存,是否保存"的对话框
        /// </summary>
        /// <returns></returns>
        public static DialogResult EnquireIsSaveCurrentDataByNew(string titel)
        {
            if (string.IsNullOrEmpty(titel)) titel = LocalData.IsEnglish ? "Tip" : "提示";

            DialogResult result = XtraMessageBox.Show(LocalData.IsEnglish ? "The new data is UnSave,Sure save?" : "新增的数据未保存,是否保存?"
                                , titel
                                , MessageBoxButtons.YesNoCancel
                                , MessageBoxIcon.Question);
            return result;
        }

        public static DialogResult ShowPartInDialog(Control control, string title)
        {
            XtraForm form = new XtraForm();
            form.Size = new Size(control.Size.Width + 5, control.Size.Height + 15);
            form.StartPosition = FormStartPosition.CenterParent;
            form.FormBorderStyle = FormBorderStyle.FixedSingle;
            form.MaximizeBox = false;
            form.Text = title;
            form.Controls.Add(control);
            control.Dock = DockStyle.Fill;
            return form.ShowDialog();
        }

        /// <summary>
        /// 显示一个提示,用户选择是/否
        /// </summary>
        /// <returns></returns>
        public static bool ShowResultMessage(string message)
        {
            DialogResult result = XtraMessageBox.Show(message,
                                               LocalData.IsEnglish ? "Tip" : "提示",
                                               MessageBoxButtons.YesNo,
                                               MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 弹出一个对话框
        /// </summary>
        /// <param name="message"></param>
        public static void ShowMessage(string message)
        {
            DialogResult result = XtraMessageBox.Show(message,
                                              LocalData.IsEnglish ? "Tip" : "提示");
        }


        /// <summary>
        /// 弹出一个是提示"是否"的对话框
        /// </summary>
        /// <returns></returns>
        public static bool EnquireYesOrNo(string message)
        {
            DialogResult result = MessageBoxService.ShowQuestion(message,"Tip",MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 弹出一个是提示"是否删除"的对话框
        /// </summary>
        /// <returns></returns>
        public static bool EnquireIsDeleteCurrentData()
        {


            DialogResult result = MessageBoxService.ShowQuestion(LocalData.IsEnglish ? "Srue Delete Current Data?" : "是否删除当前数据?",
                                                 LocalData.IsEnglish ? "Tip" : "提示", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 功能描述：打开一个模态窗口,返回该模态窗口的DialogReasult的值
        /// </summary>
        /// <param name="control"></param>
        /// <param name="title"></param>
        public static DialogResult ShowDialog(Control control, string title)
        {
            return ShowDialog(control, title, FormBorderStyle.FixedSingle);
        }

        /// <summary>
        /// 功能描述：打开一个模态窗口,返回该模态窗口的DialogReasult的值
        /// </summary>
        /// <param name="control"></param>
        /// <param name="title"></param>
        public static DialogResult ShowDialog(Control control, string title, FormBorderStyle formBorderStyle)
        {
            XtraForm form = new XtraForm();
            form.MaximizeBox = false;
            form.MinimizeBox = false;
            form.Text = title;
            form.StartPosition = FormStartPosition.CenterScreen;
            if (control.Height >= Screen.PrimaryScreen.WorkingArea.Height)
            {
                form.Size = new Size(control.Size.Width + 10, control.Size.Height + 30);
            }
            else
            {
                form.Size = new Size(control.Size.Width, control.Size.Height + 30);
            }
            form.FormBorderStyle = formBorderStyle;
            control.Dock = DockStyle.Fill;
            form.Controls.Add(control);
            return form.ShowDialog();
        }

        #endregion

        #region 设置TextEdit的水印
        /// <summary>
        /// 设置CustoemrTextEdit的水印
        /// </summary>
        /// <param name="textEdits">TextEdit的集合</param>
        public static void SetCustomerTextEditNullValuePrompt(List<TextEdit> textEdits)
        {
            string tip = LocalData.IsEnglish ? "Please Input Code or EName or CName." : "请输入代码、中文名称或英文名称.";
            SetTextEditNullValuePrompt(textEdits, tip);
        }

        /// <summary>
        /// 设置PortTextEdit的水印
        /// </summary>
        /// <param name="textEdits">TextEdit的集合</param>
        public static void SetPortTextEditNullValuePrompt(List<TextEdit> textEdits)
        {
            string tip = LocalData.IsEnglish ? "Please Input EName or CName." : "请输入中文名称或英文名称.";
            SetTextEditNullValuePrompt(textEdits, tip);
        }

        /// <summary>
        /// 设置VoyageTextTextEdit的水印
        /// </summary>
        /// <param name="textEdits">TextEdit的集合</param>
        public static void SetVoyageTextEditNullValuePrompt(List<TextEdit> textEdits)
        {
            string tip = LocalData.IsEnglish ? "Please Input Voyage or Vessel." : "请输入船名或航次.";
            SetTextEditNullValuePrompt(textEdits, tip);
        }

        /// <summary>
        /// 设置TextEdit的水印
        /// </summary>
        /// <param name="textEdits">TextEdit的集合</param>
        /// <param name="tip">水印字串,传空默认为"请输入代码、中文名称或英文名称."</param>
        public static void SetTextEditNullValuePrompt(List<TextEdit> textEdits, string tip)
        {
            if (textEdits == null || textEdits.Count == 0) return;
            if (string.IsNullOrEmpty(tip)) tip = LocalData.IsEnglish ? "Please Input Code or EName or CName." : "请输入代码、中文名称或英文名称.";
            foreach (var item in textEdits)
            {
                item.Properties.NullValuePrompt = tip;
                item.Properties.NullValuePromptShowForEmptyValue = true;
            }
        }
        #endregion

        #region 设置Grid自动换行
        public static void SetXraGridViewColWordrap(GridView p_XtraGridView, string p_FieldName)
        {
            SetXraGridViewColWordrap(p_XtraGridView, p_FieldName, false);
        }
        /// <summary>    
        /// 设置某列自动换行
        /// </summary>    
        /// <param name="p_XtraGridView">待设置的XtraGridView</param>    
        /// <param name="p_FieldName">列名</param>   
        public static void SetXraGridViewColWordrap(GridView p_XtraGridView, string p_FieldName, bool isReadOnly)
        {
            //设置允许自动换行所需的一些属性      
            GridColumn col = p_XtraGridView.Columns[p_FieldName];

            if (col == null)
            {
                return;
            }

            if (!p_XtraGridView.OptionsView.RowAutoHeight)
            {
                p_XtraGridView.OptionsView.RowAutoHeight = true;
            }
            if (!col.AppearanceCell.Options.UseTextOptions)
            {
                col.AppearanceCell.Options.UseTextOptions = true;
            }
            if (col.AppearanceCell.TextOptions.WordWrap != WordWrap.Wrap)
            {
                col.AppearanceCell.TextOptions.WordWrap = WordWrap.Wrap;
            }

            //用RepositoryItemMemoEdit控件作为显示控件,以便换行
            RepositoryItemMemoEdit res = null;

            RepositoryItem resOld = col.ColumnEdit;

            if (resOld != null)
            {

                if (resOld is RepositoryItemMemoEdit)

                    res = resOld as RepositoryItemMemoEdit;
                else
                {
                    p_XtraGridView.GridControl.RepositoryItems.Remove(resOld);
                    resOld.Dispose();
                }
            }

            if (res == null)
            {
                res = new RepositoryItemMemoEdit();
                p_XtraGridView.GridControl.RepositoryItems.Add(res);
                col.ColumnEdit = res;
            }

            res.ReadOnly=isReadOnly;

            if (isReadOnly)
            {
                res.ContextMenu = new ContextMenu();
                res.KeyDown += new KeyEventHandler(res_KeyDown);
            }
        }

        static void res_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;               
            }
        }
        #endregion

        #region  海运运价查询
        /// <summary>
        /// 查询的权限
        /// </summary>
        public static SearchPricePermission SearchOceanPermissionType
        {
            get
            {
                if (LocalCommonServices.PermissionService.HaveActionPermission(PermissionCommandConstants.SEARCHOCEAN_VIEWRESERVE))
                {
                    return SearchPricePermission.ViewReserve;
                }
                else
                {
                    return SearchPricePermission.General;
                }
            }
        }
        #endregion

        #region 转换DataRow数据
        /// <summary>
        /// GetDateTimeByDataRow
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="defaultDate">defaultDate</param>
        /// <returns>DateTime</returns>
        public static DateTime? GetDateTimeByDataRow(object value, DateTime? defaultDate)
        {
            if (value == null)
            {
                return defaultDate;
            }

            string strValue = value.ToString();

            DateTime dt = DateTime.MinValue;
            bool isSrcc = DateTime.TryParse(strValue.Trim(), out dt);
            if (isSrcc)
            {
                return dt;
            }
            else
            {
                return defaultDate;
            }
        }

        /// <summary>
        /// 获得Strign字符类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetStringByDataRow(object value)
        {
            if (value == null)
            {
                return string.Empty; ;
            }
            else
            {
                return value.ToString().Trim();
            }

        }
        /// <summary>
        /// GetBoolByDataRow
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>bool</returns>
        public static bool GetBoolByDataRow(string value)
        {
            if (string.IsNullOrEmpty(value)|| value.Trim().Length == 0) return false;

            if (value.Trim().ToUpper() == "Y") return true;

            if (value.Trim().ToUpper() == "N") return false;

            return false;
        }
        #endregion


        /*根据信息转换为object[]*/
        public static object[] GetSingleSearchResult<T>(T data, string[] returnFields)
        {
            object[] result = new object[returnFields.Length];
            for (int i = 0; i < returnFields.Length; i++)
            {
                result[i] = GetObjectPropertyValue<T>(data, returnFields[i]);
            }

            return result;
        }

        /// <summary>
        /// TagToSplitString
        /// </summary>
        public static string TagToSplitString(this object obj, string splitString)
        {
            if (obj == null) return string.Empty;

            List<Guid> input = obj as List<Guid>;
            if (input == null || input.Count == 0) return string.Empty;

            StringBuilder strBuilder = new StringBuilder();
            foreach (var item in input)
            {
                if (strBuilder.Length > 0) strBuilder.Append(splitString);
                strBuilder.Append(item.ToString());
            }

            return strBuilder.ToString();

        }
        /// <summary>
        /// ToSplitString
        /// </summary>
        public static string ToSplitString(this List<Guid> input, string splitString)
        {
            if (input == null || input.Count == 0) return string.Empty;

            StringBuilder strBuilder = new StringBuilder();
            foreach (var item in input)
            {
                if (strBuilder.Length > 0) strBuilder.Append(splitString);
                strBuilder.Append(item.ToString());
            }

            return strBuilder.ToString();

        }
        
    }


}

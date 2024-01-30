

namespace ICP.Common.UI
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using DevExpress.Utils;
    using Framework.ClientComponents.UIManagement;
    using Framework.CommonLibrary.Client;
    using Framework.CommonLibrary.Common;
    using System.Reflection;

    public class CommonUtility
    {
        public static string[] ProcessParameter(string input)
        {
            string[] result = new string[2];
            if (string.IsNullOrEmpty(input))
            {
                return result;
            }
            if (input.Contains("/"))
            {
                int indexNo = input.LastIndexOf("/");
                result[0] = input.Substring(0, indexNo);
                if (indexNo < input.Length - 1)
                {
                    result[1] = input.Substring(indexNo + 1);
                }
                else
                {
                    result[1] = string.Empty;
                }

            }
            else
            {
                result[0] = input;
                result[1] = string.Empty;
            }
            return result;

        }
        #region 处理对象

        /// <summary>
        /// 深拷贝
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

        /// <summary>
        /// 判断Guid?是否为Null或Empty
        /// </summary>
        public static bool GuidIsNullOrEmpty(Guid? id)
        {
            if (id == null || id.Value == Guid.Empty || id.Value == Constants.NewRowID)
                return true;
            else
                return false;
        }

        public static DateTime GetEndDate(DateTime date)
        {
            string dateStr = date.ToShortDateString();
            dateStr += " 23:59:59";
            return DateTime.Parse(dateStr);
        }

        /// <summary>
        /// 获取对象某一属性值的ToString,为空的引用类型返回sting.Empty
        /// </summary>
        public static string GetObjectPropertyStringValue<T>(T t, string propertyname)
        {
            object o = CommonUtility.GetObjectPropertyValue<T>(t, propertyname);
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

        #endregion

        #region 将数字类型转化为中文大写
        /// <summary>
        /// 将数字类型转化为中文大写
        /// </summary>
        /// <param name="source">需要转换的数字内容</param>
        /// <returns>转换后的中文内容</returns>
        public static string MoneyToString(decimal source)
        {
            string returnVal = "";
            string CNChar = "";
            string bit = "万仟佰拾亿仟佰拾万仟佰拾元角分";
            string num = "壹贰叁肆伍陆柒捌玖";
            int moneyMax = bit.Length + 1;

            string moneyString = source.ToString("###########.00");
            int length = moneyString.Length - 1;

            moneyString = moneyString.Replace(".", "");//去除小数点


            for (int i = moneyString.Length; i > 0; i--)
            {
                moneyMax = moneyMax - 1;
                CNChar = bit.Substring(moneyMax - 1, 1);
                int number = Convert.ToInt16(moneyString.Substring(i - 1, 1));
                if (number == 0)
                {
                    switch (CNChar)
                    {
                        case "元":
                            returnVal = CNChar + returnVal;
                            break;
                        case "万":
                            returnVal = CNChar + returnVal;
                            break;
                        case "亿":
                            returnVal = CNChar + returnVal;
                            break;
                        case "分":
                            returnVal = "整";
                            break;
                        case "角":
                            if (returnVal != "整") returnVal = "零" + returnVal;
                            break;
                        default:
                            if ((returnVal.Substring(0, 1) != "万") && (returnVal.Substring(0, 1) != "亿") && (returnVal.Substring(0, 1) != "元") && (returnVal.Substring(0, 1) != "零")) returnVal = "零" + returnVal;
                            break;
                    }
                }
                else
                {
                    returnVal = num.Substring(number - 1, 1) + CNChar + returnVal;
                }
            }
            return returnVal;
        }
        #endregion

        #region 验证文本是否指定的格式


        /// <summary>
        /// 验证是否正数或则货币
        /// </summary>
        /// <param name="pamText">验证文本</param>
        /// <returns></returns>
        public static bool IsCurrencyOrNumeric(string pamText)
        {
            if (pamText == string.Empty)
            {
                return false;
            }

            try
            {
                Regex rg = new Regex("^[\\+|-]?[[0-9]+\\.{0,1}[0-9]{0,5}$");

                return rg.IsMatch(pamText);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 验证是否是正数
        /// </summary>
        /// <param name="value">文本值</param>
        /// <returns>如果是是正数返回true,否则返回false</returns>
        public static bool IsNumeric(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            try
            {
                Regex rg = new Regex("^[0-9]*$");

                return rg.IsMatch(value);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 判断是否是邮件地址
        /// </summary>
        /// <param name="value">文本值</param>
        /// <returns>如果是邮件格式返回true,否则返回false</returns>
        public static bool IsEmail(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            try
            {
                Regex rg = new Regex("^\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$");

                return rg.IsMatch(value);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 判断是否是网址
        /// </summary>
        /// <param name="value">文本值</param>
        /// <returns>如果是网址格式返回true,否则返回false</returns>
        public static bool IsURL(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            try
            {
                Regex rg = new Regex(@"http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");

                return rg.IsMatch(value);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 判断是否是电话格式

        /// </summary>
        /// <param name="value">文本值</param>
        /// <returns>如果是电话格式返回true,否则返回false</returns>
        public static bool IsTEL(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }

            try
            {
                //Regex rg = new Regex(@"^([0-9]{1,5}[\- ]{1,5})?([0-9]{1,4}[\- ]{1,5})?([0-9]{0,8})+([\- ]{1,5}[0-9]{1,4})?$");
                Regex rg = new Regex(@"^\d{2,4}-\d{3,8}|\d{2,4}-\d{7,11}|\d{7,15}");
                return rg.IsMatch(value);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 判断输入的字符是否包含字母
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsLetter(string value)
        {
            bool flag = true;
            if (string.IsNullOrEmpty(value.Trim()))
            {
                flag = false;
            }
            try
            {
                var regex = @"[a-zA-Z]";
                flag = Regex.IsMatch(value, regex);
            }
            catch
            {
                flag = false;
            }

            return flag;
        }

        #endregion

        #region Dialog

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
        /// 弹出一个是提示"新增的数据未保存,是否保存"的对话框
        /// </summary>
        /// <returns></returns>
        public static DialogResult EnquireIsSaveCurrentDataByNew()
        {
            DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "The new data is UnSave,Sure save?" : "新增的数据未保存,是否保存?"
                                , LocalData.IsEnglish ? "Tip" : "提示"
                                , MessageBoxButtons.YesNoCancel
                                , MessageBoxIcon.Question);
            return result;
        }

        /// <summary>
        /// 弹出一个是提示"是否删除"的对话框
        /// </summary>
        /// <returns></returns>
        public static bool EnquireIsDeleteCurrentData()
        {


            DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? "Srue Delete Current Data?" : "是否删除当前数据?",
                                                 LocalData.IsEnglish ? "Tip" : "提示",
                                                 MessageBoxButtons.YesNoCancel,
                                                 MessageBoxIcon.Question);
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
        public static DialogResult ShowDialog(System.Windows.Forms.Control control, string title)
        {
            return CommonUtility.ShowDialog(control, title, System.Windows.Forms.FormBorderStyle.FixedSingle);
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

        #region UIHelp
        /// <summary>
        /// 设置行的"作废"样式
        /// </summary>
        public static void SetRowDisuseStyle(AppearanceObject a)
        {
            a.ForeColor = Color.Gray;
            a.Options.UseForeColor = true;
            a.Options.UseFont = true;
            a.Font = new Font(a.Font, FontStyle.Strikeout);
        }

        /// <summary>
        /// 绑定controls的keyDown的Enter事件到执行btnSearch的PerformClick方法
        /// </summary>
        public static void SearchPartKeyEnterToSearch(List<Control> controls, DevExpress.XtraEditors.SimpleButton btnSearch,KeyEventHandler handler)
        {
            if (controls == null || controls.Count == 0) return;
            foreach (var item in controls)
            {
                item.KeyDown += handler;
            }
        }
        public static void RemoveSearchPartKeyEnterToSearch(List<Control> controls, KeyEventHandler handler)
        {
            if (controls == null || controls.Count == 0) return;
            foreach (var item in controls)
            {
                item.KeyDown -= handler;
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

        public static Form GetFormByDataContent(IDataContentPart contentPart)
        {
            Control control = (Control)contentPart;
            if (control != null)
            {
                return control.FindForm();
            }

            return null;
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

        public delegate bool SaveDataDelegate(object obj);
        /// <summary>
        /// UI中切换行前做相应的处理.1如果有行有改变或是新增的行都提示是否保存
        /// </summary>
        /// <param name="obj">列表对象</param>
        /// <param name="dataHoster">代理中的IDataHoster</param>
        /// <param name="saveDataDelegate">当用户点是保存时要执行的方法</param>
        /// <returns></returns>
        public static bool BeforeParentChanged(object obj, IDataHoster dataHoster, SaveDataDelegate saveDataDelegate)
        {
            if (obj == null) return true;
            object objData = dataHoster.ContentPart.Current;
            if (dataHoster.ContentPart.DataSource == null || objData == null) return true;
            if (((BaseDataObject)objData).IsDirty)
            {
                DialogResult dlg = CommonUtility.EnquireIsSaveCurrentDataByUpdated();
                if (dlg == DialogResult.Yes) return saveDataDelegate(obj);
                else if (dlg == DialogResult.Cancel) return false;
                else if (dlg == DialogResult.No && ((BaseDataObject)obj).IsNew)
                { dataHoster.RemoveData(obj); return true; }
            }
            else if (((BaseDataObject)obj).IsNew)
            {
                DialogResult dlg = CommonUtility.EnquireIsSaveCurrentDataByNew();
                if (dlg == DialogResult.Yes) return saveDataDelegate(obj);
                else if (dlg == DialogResult.Cancel) return false;
                else if (dlg == DialogResult.No)
                { dataHoster.RemoveData(obj); return true; }
            }
            return true;
        }

        public static string GetString(string key, string defaultValue)
        {
            try
            {
                if (LocalData.IsEnglish)
                {
                    //查找英文资源
                    string enVal = ICP.Common.UI.Resources.Resource_EN.ResourceManager.GetString(key);
                    if (string.IsNullOrEmpty(enVal) == false)
                    {
                        return enVal;
                    }
                }
                else
                {
                    //查找中文资源
                    string cnVal = ICP.Common.UI.Resources.Resource_CN.ResourceManager.GetString(key);
                    if (string.IsNullOrEmpty(cnVal) == false)
                    {
                        return cnVal;
                    }
                }

                return defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        public static string GetString(string key, string defaultValue, params object[] args)
        {
            try
            {
                if (LocalData.IsEnglish)
                {//查找英文资源
                    string enVal = ICP.Common.UI.Resources.Resource_EN.ResourceManager.GetString(key);
                    if (string.IsNullOrEmpty(enVal) == false)
                    {
                        return string.Format(enVal, args);
                    }
                }
                else
                {//查找中文资源
                    string cnVal = ICP.Common.UI.Resources.Resource_CN.ResourceManager.GetString(key);
                    if (string.IsNullOrEmpty(cnVal) == false)
                    {
                        return string.Format(cnVal, args); ;
                    }
                }

                return defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        //public static DocumentInfo[] IsExistFileNames(List<string> fileNames, Guid operationID)
        //{
        //    List<DocumentInfo> documentlist = ClientFileService.IsExistFileNames(fileNames, operationID);
        //    if (documentlist.Count != 0)
        //    {
        //        string strPoint = "";
        //        DialogResult dlg = DevExpress.XtraEditors.XtraMessageBox.Show(LocalData.IsEnglish ? strPoint + "The same file name already exists, confirm coverage?" : strPoint + " 已存在相同的文件名，确认覆盖?", LocalData.IsEnglish ? "Confirm" : "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        //        if (dlg != DialogResult.OK)
        //        {
        //            return null;
        //        }
        //        //foreach (DocumentInfo item in documentlist)
        //        //{
        //        //    if (item.Name == document.Name)
        //        //    {
        //        //        document.Id = item.Id;
        //        //        document.UpdateDate = item.UpdateDate;
        //        //    }
        //        //}
        //    }
        //    return null;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string GetString(string key, params object[] args)
        {
            try
            {

                if (LocalData.IsEnglish)
                {//查找英文资源
                    string enVal = ICP.Common.UI.Resources.Resource_EN.ResourceManager.GetString(key);
                    if (string.IsNullOrEmpty(enVal) == false)
                    {
                        return string.Format(enVal, args);
                    }
                }
                else
                {//查找中文资源
                    string cnVal = ICP.Common.UI.Resources.Resource_CN.ResourceManager.GetString(key);
                    if (string.IsNullOrEmpty(cnVal) == false)
                    {
                        return string.Format(cnVal, args); ;
                    }
                }

                return string.Empty;
            }
            catch
            {
                return string.Empty; ;
            }
        }
        /// <summary>
        /// 显示一个提示,用户选择是/否
        /// </summary>
        /// <returns></returns>
        public static bool ShowResultMessage(string message)
        {
            DialogResult result = DevExpress.XtraEditors.XtraMessageBox.Show(message,
                                               LocalData.IsEnglish ? "Tip" : "提示",
                                               MessageBoxButtons.YesNo,
                                               MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                return true;
            else
                return false;
        }

        public delegate void SetDelegateSource();

        /// <summary>
        /// <param name="control">控件</param>
        /// <param name="method">要执行的方法</param>
        /// </summary>
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
        /// <param name="control">控件</param>
        /// <param name="method">要执行的方法</param>
        /// <param name="isInit">是否每次进入都调用</param>
        ///</summary>
        
        public static void SetEnterToExecuteOnec(Control control, SetDelegateSource method, bool isInit)
        {
            control.Enter += delegate
            {
                if (isInit)
                {
                    method();
                }
            };
        }

        #region FinderHelper

        /*根据信息转换为object[]*/
        public static object[] GetSingleSearchResult<T>(T data, string[] returnFields)
        {
            object[] result = new object[returnFields.Length];
            for (int i = 0; i < returnFields.Length; i++)
            {
                result[i] = CommonUtility.GetObjectPropertyValue<T>(data, returnFields[i]);
            }

            return result;
        }   

        /*根据信息转换为object[]*/
        public static object[] GetMultiSearchResult<T>(List<T> datas, string[] returnFields)
        {
            object[] result = new object[datas.Count];
            for (int i = 0; i < datas.Count; i++)
            {
                result[i] = CommonUtility.GetSingleSearchResult<T>(datas[i], returnFields);
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
    }

    public class ValidateDataHelper
    {
        public ValidateDataHelper(string name, DateTime? value) { Name = name; Value = value; }
        public string Name { get; set; }
        public DateTime? Value { get; set; }
    }

    public class UIModelHelper
    {
        /// <summary>
        /// 生成一个T类型的对象,并默认填充属性的值.
        /// </summary>
        /// <typeparam name="T">要生成的类型</typeparam>
        /// <returns></returns>
        public static T GetNormalObject<T>() where T : new()
        {
            T t = new T();

            Type sourceType = t.GetType();
            System.Reflection.PropertyInfo[] properties = sourceType.GetProperties();
            foreach (var property in properties)
            {
                System.Random random = new Random();
                if (property.CanWrite == false) continue;

                try
                {
                    if (property.PropertyType == typeof(string))
                    {
                        if (property.Name.Contains("Customer"))
                            property.SetValue(t, RandomHelper.Customer(), null);
                        else if (property.Name.Contains("Creaty"))
                            property.SetValue(t, RandomHelper.User(), null);
                        else if (property.Name.Contains("Currency"))
                            property.SetValue(t, RandomHelper.Currency(), null);
                        else
                            property.SetValue(t, property.Name, null);
                    }
                    else if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
                    {
                        property.SetValue(t, RandomHelper.Date(), null);
                    }
                    else if (property.PropertyType == typeof(int) || property.PropertyType == typeof(int?))
                    {
                        property.SetValue(t, random.Next(0, 5000), null);
                    }
                    else if (property.PropertyType == typeof(decimal) || property.PropertyType == typeof(decimal?))
                    {
                        property.SetValue(t, RandomHelper.Amount(), null);
                    }
                    else if (property.PropertyType == typeof(short) || property.PropertyType == typeof(short?))
                    {
                        property.SetValue(t, (short)random.Next(1, 2), null);
                    }
                    else if (property.PropertyType == typeof(Guid) || property.PropertyType == typeof(Guid?))
                    {
                        if (property.Name.ToUpper().Contains("COMPANYID"))
                            property.SetValue(t, RandomHelper.CompanyID(), null);
                        else if (property.Name.ToUpper().Contains("SolutionID".ToUpper()))
                            property.SetValue(t, RandomHelper.SolutionID(), null);
                        else if (property.Name.ToUpper().Contains("Currency".ToUpper()))
                            property.SetValue(t, RandomHelper.CurrencyID(), null);
                        else
                            property.SetValue(t, Guid.NewGuid(), null);
                    }
                    else if (property.PropertyType == typeof(bool) || property.PropertyType == typeof(bool?))
                    {
                        property.SetValue(t, RandomHelper.GetBoolen(), null);
                    }
                    else if (property.PropertyType.IsEnum)
                    {
                        property.SetValue(t, (short)random.Next(1, 2), null);
                    }
                }
                catch { continue; }

            }

            return t;
        }
    }

    public class RandomHelper
    {
        static System.Random r = null;
        public static Random Random
        {
            get
            {
                if (r == null) r = new Random();

                return r;
            }
        }

        public static Guid SolutionID()
        {
            return new Guid("B6E4DDED-4359-456A-B835-E8401C910FD0");
        }
        public static Guid CompanyID()
        {
            return new Guid("18D4697C-AA59-E011-8208-001321CC6D9F");
        }
        public static Guid CurrencyID()
        {
            return new Guid("AF34585F-3DB8-46E1-9404-B64AE9501D10");
        }

        public static string Currency()
        {
            List<string> strs = new List<string> { "USD", "RMB", "CHK", "CAD" };
            return strs[Random.Next(0, strs.Count - 1)];
        }

        public static string Customer()
        {
            List<string> strs = new List<string> { "上海爱建进出口", "恒达国贸", "嘉陵摩托", "致富帽业", " GLOBALINK IMPOR", " 广州粮油食品进", "世荣国际运输代理", " SILVA INTERNATIONAL,INC." };
            return strs[Random.Next(0, strs.Count - 1)];
        }

        public static decimal Amount()
        {
            return decimal.Parse((Random.Next(-500, 500) + r.NextDouble()).ToString("F3"));
        }

        public static string User()
        {
            List<string> strs = new List<string> { "孔海军", "周莉莉", "余英姿", "付燕", "俞虹", "曾蓉", "李旸", "雷芳", "金立", "尹佳斌" };
            return strs[Random.Next(0, strs.Count - 1)];
        }

        public static bool GetBoolen()
        {
            return Random.Next(0, 1) == 1;
        }


        public static DateTime Date()
        {
            return DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).AddDays(Random.Next(-500, 500));
        }

    }

}

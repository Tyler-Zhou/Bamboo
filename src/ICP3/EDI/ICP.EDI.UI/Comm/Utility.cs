using System;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ICP.EDI.UI
{
    /// <summary>
    /// 工具类
    /// </summary>
    public class Utility
    {
   

        #region 自动绑定
        /// <summary>
        /// 自动绑定
        /// </summary>
        /// <param name="parentControl"></param>
        /// <param name="datasource"></param>
        public static void AutoBind(Control parentControl, DataRow datasource)
        {

            if (IsContainer(parentControl) == false)
            {
                //
                string property = GetPascalProperty(parentControl.Name);
                if (property != string.Empty)
                {
                    if (datasource.Table.Columns.Contains(property))
                    {
                        BindControl(parentControl, datasource, property);
                        SetControlMaxLenght(parentControl, datasource, property);
                    }
                }
            }
            else
            {
                foreach (Control ctrl in parentControl.Controls)
                {
                    AutoBind(ctrl, datasource);
                }
            }
        }

        /// <summary>
        /// 自动绑定
        /// </summary>
        /// <param name="parentControl"></param>
        /// <param name="datasource"></param>
        public static void AutoBind(Control parentControl, object datasource)
        {

            if (IsContainer(parentControl) == false)
            {
                //
                string property = GetPascalProperty(parentControl.Name);
                if (property != string.Empty)
                {
                    PropertyInfo[] properityInfos = datasource.GetType().GetProperties();

                    if (IsContainProperty(properityInfos, property))
                    {
                        BindControl(parentControl, datasource, property);
                        //SetControlMaxLenght(parentControl, datasource, property);
                    }
                }
            }
            else
            {
                foreach (Control ctrl in parentControl.Controls)
                {
                    AutoBind(ctrl, datasource);
                }
            }
        }

        static bool IsContainProperty(PropertyInfo[] properityInfos, string property)
        {
            foreach (PropertyInfo propertyInfo in properityInfos)
            {
                if (propertyInfo.Name == property)
                {
                    return true;
                }
            }
            return false;
        }

        public static void SetControlMaxLenght(Control ctrl, DataRow datasource, string property)
        {
            switch (ctrl.GetType().Name)
            {
                case "TextBox":
                    if (datasource.Table.Columns[property].MaxLength > 0)
                    {
                        ((TextBox)ctrl).MaxLength = datasource.Table.Columns[property].MaxLength;
                    }
                    break;
                case "CheckBox":
                    break;
                case "DateTimePicker":
                    break;
                case "ComboBox":
                    break;
                case "FlowLayoutPanel":
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctrl"></param>
        /// <param name="property"></param>
        private static void BindControl(Control ctrl, object obj, string datamember)
        {
            //清除原绑定

            ctrl.DataBindings.Clear();
            //
            switch (ctrl.GetType().Name)
            {
                case "TextBox":
                    ctrl.DataBindings.Add("Text", obj, datamember);
                    break;
                case "CheckBox":
                    ctrl.DataBindings.Add("Checked", obj, datamember);
                    break;
                case "DateTimePicker":
                    ctrl.DataBindings.Add("Value", obj, datamember);
                    break;
                case "ComboBox":
                    ctrl.DataBindings.Add("SelectedValue", obj, datamember);
                    break;
                case "FlowLayoutPanel":
                    BindFlowLayoutPanel((ctrl as FlowLayoutPanel), obj, datamember);
                    break;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="flp"></param>
        /// <param name="obj"></param>
        /// <param name="datamember"></param>
        private static void BindFlowLayoutPanel(FlowLayoutPanel flp, object obj, string datamember)
        {
            for (int i = 0; i < flp.Controls.Count; i++)
            {
                RadioButton radBut = flp.Controls[i] as RadioButton;
                if (radBut != null && radBut.Tag != null)
                {
                    int tag = short.Parse(radBut.Tag.ToString());
                    int val;
                    if (obj is DataRow)
                    {
                        val = short.Parse((obj as DataRow)[datamember].ToString());
                        if (tag == val)
                        {
                            radBut.Checked = true;
                            break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 取消绑定
        /// </summary>
        /// <param name="parentControl"></param>
        /// <param name="datasource"></param>
        public static void AutoCancelBind(Control parentControl)
        {

            if (!parentControl.HasChildren)
            {
                //
                if (parentControl.DataBindings.Count > 0)
                {
                    parentControl.DataBindings.Clear();
                }
            }
            else
            {
                foreach (Control ctrl in parentControl.Controls)
                {
                    AutoCancelBind(ctrl);
                }
            }
        }
        #endregion

        #region 自动填充

        public static void ClearControlValue(Control parent)
        {
            if (!parent.HasChildren)
            {
                switch (parent.GetType().Name)
                {
                    case "TextBox":
                        parent.Text = string.Empty;
                        break;
                    case "CheckBox":
                        CheckBox cb = (parent as CheckBox);
                        if (cb != null) cb.Checked = false;
                        break;
                    case "DateTimePicker":
                        DateTimePicker dt = (parent as DateTimePicker);
                        if (dt != null) dt.Value = DateTime.Now;
                        break;
                   
                }
            }
            else
            {
                foreach (Control ctrl in parent.Controls)
                {
                    ClearControlValue(ctrl);
                }
            }
        }

        /// <summary>
        /// 自动填充
        /// </summary>
        /// <param name="parentControl"></param>
        /// <param name="datasource"></param>
        public static void AutoFill(Control parentControl, DataRow datasource)
        {
            if (IsContainer(parentControl) == false)
            {
                //
                string property = GetPascalProperty(parentControl.Name);
                //如果属性为标准属性

                if (property != string.Empty)
                {
                    if (datasource.Table.Columns.Contains(property))//如果数据源包含此列

                    {
                        FillControl(parentControl, datasource, property);
                    }
                }
            }
            else
            {
                foreach (Control ctrl in parentControl.Controls)
                {
                    AutoFill(ctrl, datasource);
                }
            }
        }
        /// <summary>
        /// 填充数据源

        /// </summary>
        /// <param name="parentControl"></param>
        /// <param name="obj"></param>
        /// <param name="property"></param>
        private static void FillControl(Control ctrl, DataRow obj, string property)
        {
            //
            switch (ctrl.GetType().Name)
            {
                //case "TextBox":
                //    row[property]=(ctrl as TextBox).Text.ToString();
                //    break;
                //case "CheckBox":
                //    row[property] = (ctrl as CheckBox ).Checked ;
                //    break;
                //case "DateTimePicker":
                //    row[property] = (ctrl as DateTimePicker ).Value ;
                //    break;
                case "FlowLayoutPanel":
                    FillFlowLayoutPanel((ctrl as FlowLayoutPanel), obj, property);
                    break;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="flp"></param>
        /// <param name="obj"></param>
        /// <param name="datamember"></param>
        private static void FillFlowLayoutPanel(FlowLayoutPanel flp, DataRow obj, string datamember)
        {
            for (int i = 0; i < flp.Controls.Count; i++)
            {
                RadioButton radBut = flp.Controls[i] as RadioButton;
                if (radBut != null && radBut.Tag != null)
                {
                    if (radBut.Checked)
                    {
                        obj[datamember] = short.Parse(radBut.Tag.ToString());
                    }
                }
            }

        }
        #endregion

        #region 对象属性值的Copy
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
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

        #region  填充/绑定公用方法
        /// <summary>
        /// 变成Pascal
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static string ToPascalProperty(string name)
        {
            //小写
            if (name.Substring(0, 1) == name.Substring(0, 1).ToLower())
            {
                name = name.Substring(0, 1).ToUpper() + name.Substring(1, name.Length - 1);
            }
            return name;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static string GetPascalProperty(string name)
        {
            //
            char[] chars = name.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (Char.IsUpper(chars[i]))
                {
                    return name.Substring(i);
                }
            }
            return string.Empty;
        }
        /// <summary>
        /// 是否为容器

        /// </summary>
        /// <param name="ctrl"></param>
        /// <returns></returns>
        private static bool IsContainer(Control ctrl)
        {
            if (ctrl.HasChildren == false) return false;
            //如果是FlowLayoutPancel
            if (ctrl is FlowLayoutPanel && ctrl.Controls.Count > 0)
            {
                bool containerOther = false;
                foreach (Control c in ctrl.Controls)
                {
                    if (!(c is RadioButton))
                    {
                        containerOther = true;
                    }
                }
                if (!containerOther) return false;
            }
            //该控件为容器
            return true;
        }
        #endregion

        #region 刷新行，
        /// <summary>
        /// 刷新行

        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        public static void RefershRow(DataRow source, DataRow dest)
        {
            foreach (DataColumn col in dest.Table.Columns)
            {
                if (source.Table.Columns.Contains(col.ColumnName))
                {
                    dest[col.ColumnName] = source[col.ColumnName];
                }
            }
        }

        #endregion

        #region  处理 DBNull
        /// <summary>
        /// 处理DBNull
        /// </summary>
        /// <remarks >
        /// 因为从数据库中获取的集合，往往有不少DBNull,但在绑定时，容易出错
        /// ?为什么不在获取数据时，将Null填充？

        /// 因为在客户端处理,对数和据库性能影响更小
        /// </remarks>
        /// <param name="source"></param>
        public static void ProcessDBNull(DataTable source)
        {
            foreach (DataRow dr in source.Rows)
            {
                foreach (DataColumn col in source.Columns)
                {
                    if (dr[col.ColumnName] == DBNull.Value
                        || (col.DataType.Name == "DateTime" && ((DateTime)dr[col.ColumnName]).Year < 1900))
                    {
                        switch (col.DataType.Name)
                        {
                            case "String":
                                dr[col.ColumnName] = string.Empty;
                                break;
                            case "Decimal":
                                dr[col.ColumnName] = 0.0m;
                                break;
                            case "Int16":
                                dr[col.ColumnName] = 0;
                                break;
                            case "Int32":
                                dr[col.ColumnName] = 0;
                                break;
                            case "DateTime":
                                dr[col.ColumnName] = DateTime.Parse("1900-1-1");
                                break;
                            case "Guid":
                                dr[col.ColumnName] = Guid.Empty;
                                break;
                            case "Boolean":
                                dr[col.ColumnName] = false;
                                break;
                        }
                    }
                }
            }
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

        #region  将页面只读

        /// <summary>
        /// 页面只读
        /// </summary>
        /// <param name="parentControl"></param>
        public static void ReadOnlyPart(Control parentControl)
        {
            ProcessPart(parentControl, true);
        }
        /// <summary>
        /// 解开只读
        /// </summary>
        /// <param name="parentControl"></param>
        public static void UnReadOnlyPart(Control parentControl)
        {
            ProcessPart(parentControl, false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentControl"></param>
        /// <param name="isReadOnly"></param>
        private static void ProcessPart(Control parentControl, bool isReadOnly)
        {
            if (IsContainer(parentControl) == false)
            {
                switch (parentControl.GetType().Name)
                {
                    case "TextBox":
                        (parentControl as TextBox).ReadOnly = isReadOnly;
                        break;
                    case "Button":
                        (parentControl as Button).Enabled = !isReadOnly;
                        break;
                    case "DateTimePicker":
                        (parentControl as DateTimePicker).Enabled = !isReadOnly;
                        break;
                   
                    case "CheckBox":
                        (parentControl as CheckBox).Enabled = !isReadOnly;
                        break;
                    case "RadioButton":
                        (parentControl as RadioButton).Enabled = !isReadOnly;
                        break;
                    case "ComboBox":
                        (parentControl as ComboBox).Enabled = !isReadOnly;
                        break;
                  
                }
            }
            else
            {
                foreach (Control ctrl in parentControl.Controls)
                {
                    ProcessPart(ctrl, isReadOnly);
                }
            }
        }
        #endregion

        #region 注册事件
        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="parentControl"></param>
        public static void RegistEnterEvent(Control parentControl, KeyEventHandler TextBox_KeyDown)
        {
            if (parentControl.HasChildren)
            {
                foreach (Control ctrl in parentControl.Controls)
                {
                    RegistEnterEvent(ctrl, TextBox_KeyDown);
                }
            }
            else if (parentControl is TextBox)
            {
                (parentControl as TextBox).KeyDown += new KeyEventHandler(TextBox_KeyDown);
            }
        }

        #endregion

        #region 打开弹出框

        /// <summary>
        /// 功能描述：打开一个模态窗口,但是不能返回该模态窗口的DialogReasult的值

        /// </summary>
        /// <param name="control"></param>
        /// <param name="title"></param>
        public static void ShowDialog(System.Windows.Forms.Control control, string title)
        {
            Form form = new Form();
            form.MaximizeBox = false;
            form.MinimizeBox = false;
            form.Text = title;
            form.StartPosition = FormStartPosition.CenterScreen;
            if (control.Height >= Screen.PrimaryScreen.WorkingArea.Height)
            {
                form.Size = new System.Drawing.Size(control.Size.Width + 10, control.Size.Height + 30);
            }
            else
            {
                form.Size = new System.Drawing.Size(control.Size.Width, control.Size.Height + 30);
            }
            form.FormBorderStyle = FormBorderStyle.FixedSingle;
            form.Controls.Add(control);
            form.ShowDialog();
        }
        #endregion

        #region 控制字符输入数字

        /// <summary>
        /// 控制字符输入数字
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">e</param>
        /// <param name="decimalIntegerLength">小数点前位数</param>
        /// <param name="decimalFractionLength">小数点后位数</param>
        /// <param name="isPositive">是否可为负数</param>
        public static void Decimal_KeyPress(object sender, KeyPressEventArgs e, int decimalIntegerLength, int decimalFractionLength, bool isPositive)
        {
            try
            {
                if (Convert.ToInt32(e.KeyChar) == 8) return;

                TextBox tb = sender as TextBox;

                int iNum1 = decimalFractionLength;//从配置服务器端获取字段小数位数





                int iNum2 = decimalIntegerLength;//整数位数
                string strSymbol = "";

                if (isPositive)
                {
                    strSymbol = "^([0]|[-]([1-9]\\d{0," + iNum2 + "})?|([1-9]\\d{0," + iNum2 + "})?)([.][0-9]{0," + iNum1 + "})?$";
                }
                else
                {
                    strSymbol = "^([0]([1-9]\\d{0," + iNum2 + "})?|([1-9]\\d{0," + iNum2 + "})?)([.][0-9]{0," + iNum1 + "})?$";
                }


                string str = "";
                str = tb.Text.Substring(0, tb.SelectionStart) +
                    e.KeyChar +
                    tb.Text.Substring(tb.SelectionStart, tb.Text.Length - tb.SelectionStart - tb.SelectionLength);

                e.Handled = !(System.Text.RegularExpressions.Regex.IsMatch(str, strSymbol));
            }
            catch { }
        }

        #endregion

        #region 消息框

        public static void ShowMessageBox(string key, string defaultValue)
        {
            MessageBox.Show(Utility.GetValueFromResource(key, defaultValue));
        }

        public static void ShowMessageBox(string key, string defaultValue, string tilte)
        {
            MessageBox.Show(Utility.GetValueFromResource(key, defaultValue), tilte);
        }

        public static DialogResult ShowMessageBox(string key, string defaultValue, string title, MessageBoxButtons buttns, MessageBoxIcon icon)
        {
            return MessageBox.Show(Utility.GetValueFromResource(key, defaultValue), title, buttns, icon);
        }

        public static DialogResult ShowMessageBox(string key, string defaultValue, string title, MessageBoxButtons buttns, MessageBoxIcon icon, MessageBoxDefaultButton btn)
        {
            return MessageBox.Show(Utility.GetValueFromResource(key, defaultValue), title, buttns, icon, btn);
        }

        #endregion

        #region 中英文支持


        /// <summary>
        /// 判断当前环境是否在英文环境下
        /// </summary>
        public static bool IsEnglish
        {
            get
            {
                return System.Threading.Thread.CurrentThread.CurrentCulture.Name.Contains("en");
            }
        }

        /// <summary>
        /// 根据关键字查找中英文资源
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="defaultValue">如果在当前语言环境中，没找到，就设置为当前的默认值</param>
        /// <returns></returns>
        public static string GetValueFromResource(string key, string defaultValue)
        {
            try
            {
                //if (System.Threading.Thread.CurrentThread.CurrentCulture.Name.Contains("en") == true)
                //{//查找英文资源
                //    string enVal = ICP.EDI..OTSResourceEN.ResourceManager.GetString(key);
                //    if (enVal.Trim() != string.Empty)
                //    {
                //        return ICP.OTS.Export.WinUI.Resources.OTSResourceEN.ResourceManager.GetString(key);
                //    }
                //}
                //else
                //{//查找中文资源
                //    string chnVal = ICP.OTS.Export.WinUI.Resources.OTSResourceCHN.ResourceManager.GetString(key);
                //    if (chnVal.Trim() != string.Empty)
                //    {
                //        return ICP.OTS.Export.WinUI.Resources.OTSResourceCHN.ResourceManager.GetString(key);
                //    }
                //}

                return defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        public static string GetDateTimeFormat()
        {
            if (System.Globalization.CultureInfo.CurrentCulture.LCID == 2052)
            {
                return "yyyy-MM-dd";
            }
            return "MM/dd/yyyy";
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

        public static bool IsCurrencyOrNumeric1(string pamText)
        {
            if (pamText == string.Empty)
            {
                return false;
            }

            try
            {
                Regex rg = new Regex("^[0-9]+\\.{0,1}[0-9]{0,5}$");

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
        /// <param name="pamText"></param>
        /// <returns></returns>
        public static bool IsNumeric(string pamText)
        {
            if (pamText == string.Empty)
            {
                return false;
            }

            try
            {
                Regex rg = new Regex("^[0-9]*$");

                return rg.IsMatch(pamText);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 判断是否是邮件地址
        /// </summary>
        /// <param name="pamText"></param>
        /// <returns></returns>
        public static bool IsEmail(string pamText)
        {
            if (pamText == string.Empty)
            {
                return false;
            }

            try
            {
                Regex rg = new Regex("^\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$");

                return rg.IsMatch(pamText);
            }
            catch
            {
                return false;
            }
        }


        #endregion
    }



    public class OutlookEMail
    {
        [System.Runtime.InteropServices.DllImport("shell32.dll", EntryPoint = "ShellExecuteA")]
        public static extern int ShellExecute(
         int hwnd,
         String lpOperation,
         String lpFile,
         String lpParameters,
         String lpDirectory,
         int nShowCmd
         );


    }
}

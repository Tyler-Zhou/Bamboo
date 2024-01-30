using System.Windows.Forms;
using ICP.Framework.CommonLibrary.Client;
using ICP.WF.Controls;
using DevExpress.XtraEditors;
using ICP.Common.ServiceInterface.Client;
using Microsoft.Practices.CompositeUI;
using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ICP.WF.UI
{
    public class Utility
    {

        public WorkItem Workitem
        {
            get
            {
                return ServiceClient.GetClientService<WorkItem>();
            }
        }

        public IReportViewService ReportViewService
        {
            get { return ServiceClient.GetClientService<IReportViewService>(); }
        }

        /// <summary>
        /// 根据关键字查找中英文资源
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="defaultValue">如果在当前语言环境中，没找到，就设置为当前的默认值</param>
        /// <returns></returns>
        public static string GetString(string key, string defaultValue)
        {
            try
            {
                if (ICP.Framework.CommonLibrary.Client.LocalData.IsEnglish)
                {//查找英文资源
                    string enVal = ICP.WF.UI.Resources.Resource_EN.ResourceManager.GetString(key);
                    if (string.IsNullOrEmpty(enVal) == false)
                    {
                        return enVal;
                    }
                }
                else
                {//查找中文资源
                    string cnVal = ICP.WF.UI.Resources.Resource_CN.ResourceManager.GetString(key);
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

        /// <summary>
        /// 弹出一个是提示"是否删除"的对话框
        /// </summary>
        /// <returns></returns>
        public static bool EnquireIsDeleteCurrentData()
        {
            return EnquireIsDeleteCurrentData("DeleteData");
        }

        /// <summary>
        /// 弹出一个是提示"是否删除"的对话框
        /// </summary>
        /// <returns></returns>
        public static bool EnquireIsDeleteCurrentData(string message)
        {
            message = GetString(message, message);
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
        /// 设置容器控件的所有子控件只读
        /// </summary>
        /// <param name="parentControl"></param>
        /// <param name="isReadOnly"></param>
        public static void SetReadOnly(Control parentControl, bool isReadOnly)
        {
            if (IsContainer(parentControl) == false)
            {
                switch (parentControl.GetType().Name)
                {
                    case "TextBox":
                        (parentControl as LWTextBox).ReadOnly = isReadOnly;
                        break;
                    case "LWMultiTextBox":
                        (parentControl as LWMultiTextBox).ReadOnly = isReadOnly;
                        break;
                    case "TextBoxMaskBox":
                        (parentControl as TextBoxMaskBox).ReadOnly = isReadOnly;
                        break;
                    case "LWComboBoxTreeView":
                        (parentControl as LWComboBoxTreeView).ReadOnly = !isReadOnly;
                        break;
                    case "LWNumericCalcEdit":
                        (parentControl as LWNumericCalcEdit).ReadOnly = isReadOnly;
                        break;
                    case "LWTextBox":
                        (parentControl as LWTextBox).ReadOnly = isReadOnly;
                        break;
                    case "LWRadioGroup":
                        (parentControl as LWRadioGroup).Enabled = !isReadOnly;
                        break;
                    case "LWRadioButton":
                        (parentControl as LWRadioButton).Enabled = !isReadOnly;
                        break;
                    case "LWDatePicker":
                        (parentControl as LWDatePicker).ReadOnly = !isReadOnly;
                        break;
                    case "LWCheckBox":
                        (parentControl as LWCheckBox).ReadOnly = !isReadOnly;
                        break;
                    case "LWComBox":
                        (parentControl as LWComBox).ReadOnly = !isReadOnly;
                        break;
                    case "LWDataGridView":
                        (parentControl as LWDataGridView).AllowUserToAddRows = false;
                        (parentControl as LWDataGridView).ReadOnly = isReadOnly;
                        break;
                }
            }
            else
            {
                foreach (Control ctrl in parentControl.Controls)
                {
                    SetReadOnly(ctrl, isReadOnly);
                }
            }
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
        /// 业务费用
        /// </summary>
        public static Guid BussinessExpense = new Guid("1ACB0832-9118-46FF-93B0-9E5038283D13");
        /// <summary>
        /// 非业务费用
        /// </summary>
        public static Guid NotBussinessExpense = new Guid("DEA9B18D-AEC2-4A00-8482-FB827082FA38");
        /// <summary>
        /// 非业务费用-黄晖
        /// </summary>
        public static Guid NotBussinessExpenseHH = new Guid("9F81BA32-9A98-4E62-86E8-C46E2EFF2D75");
        /// <summary>
        /// 其它业务支出
        /// </summary>
        public static Guid OtherBussinessExpense = new Guid("565B6F73-9C68-4DB1-A437-E814C89B687E");
        /// <summary>
        /// 提成发放
        /// </summary>
        public static Guid RoyaltyExpense = new Guid("B60E9D0A-0C35-46CF-82ED-06BBD3736858");
        /// <summary>
        /// 借款申请
        /// </summary>
        public static Guid LoanExpense = new Guid("3CD2D8A3-EDB6-44AA-909D-17F898BF5B81");
        /// <summary>
        /// 固定资产
        /// </summary>
        public static Guid FixedAssetsExpense = new Guid("AC08A35C-BEEC-4E5A-9E12-76118403FAAF");
        /// <summary>
        /// 影视费用报销
        /// </summary>
        public static Guid MovieExpense = new Guid("6CE853C7-A4B0-E311-B8A2-0014C25F84B3");
        /// <summary>
        /// 物流部资金调拨流程
        /// </summary>
        public static Guid Allocation = new Guid("C1B88DB8-9B39-430E-AE41-0F8182E95A99");

        /// <summary>
        /// 财务审核表单ID
        /// </summary>
        public static Guid NewAccountAuditorFormID = new Guid("9376CA1F-259D-E311-A60E-0014C25F84B3");
        /// <summary>
        /// 会计审核表单ID
        /// </summary>
        public static Guid NewCashierAuditorFormID = new Guid("B9539FBF-259D-E311-A60E-0014C25F84B3");


        #region 币种

        /// <summary>
        /// 人民币
        /// </summary>
        public static Guid RMBID = new Guid("DEB5F402-B6C0-4491-B247-B75C3EDA7976");
        /// <summary>
        /// 港币
        /// </summary>
        public static Guid HKDID = new Guid("9D0EF37F-9C69-4368-80B1-62F2D5C669AC");
        /// <summary>
        /// 美元
        /// </summary>
        public static Guid USDID = new Guid("D67186CE-8B2C-4A75-81F1-A4FE3CC12DE9");


        /// <summary>
        /// 现金科目--人民币
        /// </summary>
        public static Guid RMBCashGLID = new Guid("AB120B18-B33C-4941-AE11-9090BA32E120");
        /// <summary>
        /// 长沙现金科目--人民币
        /// </summary>
        public static Guid RMBCSCashGLID = new Guid("48ACB296-5BC1-E311-B530-0026551CA878");
        /// <summary>
        /// 现金科目--港币
        /// </summary>
        public static Guid HKDCashGLID = new Guid("F7C86566-0EF2-444D-9A14-66EDF5EA968D");
        /// <summary>
        /// 现金科目--美元
        /// </summary>
        public static Guid USDCashGLID = new Guid("22E64EDF-A801-41D2-93D2-21CD955DD2F9");
        /// <summary>
        /// 银行存款--马币
        /// </summary>
        public static Guid RMBankGLID = new Guid("59C1BA9C-91EF-E211-8148-0014C25F84B3");


        /// <summary>
        /// 内部往来-人民币
        /// </summary>
        public static Guid IntercompanyRMBID = new Guid("574B2056-8BCA-4ABA-AF8F-D332E6F6856E");
        /// <summary>
        /// 内部往来-港币
        /// </summary>
        public static Guid IntercompanyHKDID = new Guid("A96056AE-C440-4D6F-A33A-3B55D5A884B4");
        /// <summary>
        /// 内部往来-美元
        /// </summary>
        public static Guid IntercompanyUSDID = new Guid("CC4C5320-DD53-4BE7-9464-B0ADFE92F80F");
        #endregion

        /// <summary>
        /// 是否为容器
        /// </summary>
        /// <param name="ctrl"></param>
        /// <returns></returns>
        private static bool IsContainer(Control ctrl)
        {
            if (ctrl.HasChildren == false) return false;
            if (ctrl is LWDataGridView) return false;
            if (ctrl is LWRadioGroup) return false;

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
        /// <summary>
        /// 是否为数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsValidNumber(string str)
        {
            return Regex.IsMatch(str, "^[0-9]*[1-9][0-9]*$");
        }
    }
}

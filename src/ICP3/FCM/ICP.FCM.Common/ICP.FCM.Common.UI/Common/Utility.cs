using DevExpress.XtraEditors;
using ICP.Business.Common.UI.Communication;
using ICP.Business.Common.UI.Document;
using ICP.Business.Common.UI.UpdateETA;
using ICP.FCM.Common.ServiceInterface;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.UI.BillRevise;
using ICP.FCM.Common.UI.Common.Parts;
using ICP.FCM.Common.UI.DispatchCompare;
using ICP.FCM.Common.UI.Document;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.ClientComponents.Forms;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ICP.FCM.Common.UI
{
    /// <summary>
    /// FCM 工具类
    /// </summary>
    public class FCMUIUtility
    {
        /// <summary>
        /// 根据Key从集合中获取值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static object GetValue(string key, IDictionary<string, object> values)
        {
            if (values == null)
                return null;
            if (values.ContainsKey(key))
            {
                return values[key];
            }
            return null;
        }

        /// <summary>
        /// 通过对字符的unicode编码进行判断来确定字符是否为中文.
        /// 在unicode 字符串中，中文的范围是在4E00..9FFF:
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <returns>返回是否含有中文</returns>
        public static bool IsContainsChinese(string input)
        {
            if (string.IsNullOrEmpty(input)) return false;

            int chfrom = Convert.ToInt32("4e00", 16);    //范围（0x4e00～0x9fff）转换成int（chfrom～chend）
            int chend = Convert.ToInt32("9fff", 16);
            for (int i = 0; i < input.Length; i++)
            {
                int code = Char.ConvertToUtf32(input, i);
                if (code >= chfrom && code <= chend)
                    return true;
            }

            return false;
        }
        /// <summary>
        /// 设置数据源
        /// </summary>
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
                method = null;

            };
        }

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

        #region
        public static void SetDocumentListDataSource(UCDocumentList documentList, MemoParam para)
        {
            
            BusinessOperationContext context = BusinessOperationContext.Current;
            context.OperationID = para.OperationId;
            context.OperationType = para.OperationType;
            context.FormType = para.FormType;
            context.Add("DocumentState", para.State);
            documentList.Presenter.BindData(context);
        }
        public static void SetDocumentListDataSource(UCDocumentList documentList, BusinessOperationContext operationContext)
        {
          documentList.Presenter.BindData(operationContext);
        }

        #endregion


        #region Fax/Mail/EDI沟通历史记录  BL提单
        public static void SetCommunicationDataSource(UCCommunicationHistory communicationCtl, MemoParam memoParam)
        {
            BusinessOperationContext context = BuildContext(memoParam);
            communicationCtl.BindData(context);
        }



        public static void SetUCDocumentDispatchDataSource(UCDocumentDispatchPart docDispatch, MemoParam memoParam)
        {
            BusinessOperationContext context = BuildContext(memoParam);
            docDispatch.DataSource = context;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="memoParam"></param>
        /// <returns></returns>
        public static BusinessOperationContext BuildContext(MemoParam memoParam)
        {
            BusinessOperationContext context = BusinessOperationContext.Current;
            context.OperationID = memoParam.OperationId;
            context.OperationType = memoParam.OperationType;
            context.FormType = memoParam.FormType;
            context.Add("DocumentState", memoParam.State);
            return context;
        }

        #endregion

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

        #region FinderHelper

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

        /*根据信息转换为object[]*/
        public static object[] GetMultiSearchResult<T>(List<T> datas, string[] returnFields)
        {
            object[] result = new object[datas.Count];
            for (int i = 0; i < datas.Count; i++)
            {
                result[i] = GetSingleSearchResult<T>(datas[i], returnFields);
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

        

        #region 分文件或修订
        /// <summary>
        /// 弹出分发文档界面
        /// </summary>
        /// <param name="workitem"></param>
        /// <param name="context"></param>
        /// <param name="editPartSaved"></param>
        /// <param name="type"></param>
        public static void ShowDispatchDocumentNew(WorkItem workitem, BusinessOperationContext context, PartDelegate.EditPartSaved editPartSaved, int type)
        {
            IntPtr agentDispatchMailForm;
            PopupWindow form = null;
            FrmSendAgentDocumentNew frmDocument = workitem.SmartParts.AddNew<FrmSendAgentDocumentNew>();
            frmDocument.DataSource = context;
            frmDocument.workflag = type;
            form = new PopupWindow();
            form.MaximizeBox = form.MinimizeBox = false;
            form.ShowIcon = true;
            form.ShowInTaskbar = true;
            form.KeyPreview = true;
            form.KeyDown += new KeyEventHandler(form_KeyDown);
            form.FormBorderStyle = FormBorderStyle.FixedSingle;
            form.Text = LocalData.IsEnglish ? "Dispatch Document" : "分发文档";
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ErrorListControl.Visible = false;
            frmDocument.Dock = DockStyle.Fill;
            form.Width = frmDocument.Width + 5;
            form.Height = frmDocument.Height + 60;
            form.Controls.Add(frmDocument);
            agentDispatchMailForm = form.Handle;
            if (editPartSaved != null)
            {
                frmDocument.Saved += delegate (object[] prams)
                {
                    editPartSaved(prams);
                };
            }
            form.ShowDialog();
        }

        /// <summary>
        /// 弹出分发文档签收比较界面
        /// </summary>
        /// <param name="workitem"></param>
        /// <param name="OEOperationID"></param>
        /// <param name="OIOperationID"></param>
        /// <param name="isVisibleAccept"></param>
        public static void ShowAcceptedDocumentCompareNew(WorkItem workitem, Guid OEOperationID, Guid OIOperationID, bool isVisibleAccept)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");

            try
            {

                OIDispachCompareNew mainSpce = workitem.SmartParts.AddNew<OIDispachCompareNew>();
                mainSpce.NewOperationID = OEOperationID;
                mainSpce.OldOperationID = OIOperationID;
                mainSpce.IsVisibleAccept = isVisibleAccept;


                IWorkspace mainWorkspace = workitem.Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Accept Dispatched Docs From The Agent" : "签收代理分发的文档";
                mainWorkspace.Show(mainSpce, smartPartInfo);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(LocalData.IsEnglish ? "Accept Dispatched Docs From The Agent is opened,can't open it again" : "签收代理分发的文档已打开，不能再次打开");
            }
            LoadingServce.CloseLoadingForm(theradID);

        }

        /// <summary>
        /// 弹出修订签收比较界面
        /// </summary>
        /// <param name="workitem">WorkItem</param>
        /// <param name="OEOperationID">海出操作ID</param>
        public static void ShowReviseAccepteNew(WorkItem workitem, Guid OEOperationID)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");
            try
            {
                OEBillRevisePartNew mainSpce = workitem.SmartParts.AddNew<OEBillRevisePartNew>();
                mainSpce.NewOperationID = OEOperationID;
                IWorkspace mainWorkspace = workitem.Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Accept Revised D/C Fees From The Agent" : "签收代理修订的费用";
                mainWorkspace.Show(mainSpce, smartPartInfo);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(LocalData.IsEnglish ? "Accept Revised D/C Fees From The Agent is opened,can't open it again" : "签收代理修订的费用已打开，不能再次打开");
            }
            LoadingServce.CloseLoadingForm(theradID);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Workitem"></param>
        /// <param name="operationID"></param>
        /// <param name="AgentName"></param>
        public static void ShowBillRevise(WorkItem Workitem, Guid operationID, string AgentName)
        {
            IntPtr agentDispatchMailForm;
            PopupWindow form = null;// new DispatchDocumentCompare(workitem);//
            OIApplyRevise frmDocument = Workitem.SmartParts.AddNew<OIApplyRevise>();

            frmDocument.OperationID = operationID;
            frmDocument.AgentName = AgentName;
            form = new PopupWindow();
            form.MinimizeBox = false;
            form.ShowIcon = true;
            form.ShowInTaskbar = false;
            form.KeyPreview = true;
            form.KeyDown += new KeyEventHandler(form_KeyDown);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.Text = LocalData.IsEnglish ? "Revise Agent Fees" : "修订代理费用";
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ErrorListControl.Visible = false;
            frmDocument.Dock = DockStyle.Fill;
            form.Width = form.Width + 10;
            form.Height = form.Height + 10;
            form.AcceptButton = frmDocument.BtnApplyRevise;
            form.CancelButton = frmDocument.BtnCancel;
            form.Controls.Add(frmDocument);
            agentDispatchMailForm = form.Handle;

            form.ShowDialog();
        }
        /// <summary>
        /// 弹出历史分发文档签收比较界面
        /// </summary>
        /// <param name="workitem"></param>
        /// <param name="OperationID"></param>
        /// <param name="BeforeApplyID"></param>
        /// <param name="AfterApplyID"></param>
        public static void ShowHistoryDocumentCompare(WorkItem workitem, Guid OperationID, Guid BeforeApplyID, Guid AfterApplyID)
        {
            int theradID = 0;
            theradID=LoadingServce.ShowLoadingForm("Loading...");
            OIAcceptHistoryCompare mainSpce = workitem.SmartParts.AddNew<OIAcceptHistoryCompare>();
            mainSpce.OperationID = OperationID;
            mainSpce.BeforeApplyID = BeforeApplyID;
            mainSpce.AfterApplyID = AfterApplyID;

            IWorkspace mainWorkspace = workitem.Workspaces[ClientConstants.MainWorkspace];
            SmartPartInfo smartPartInfo = new SmartPartInfo();
            smartPartInfo.Title = LocalData.IsEnglish ? "Accept Agent Dispatch History Compare" : "签收代理分发历史比较";
            mainWorkspace.Show(mainSpce, smartPartInfo);
            LoadingServce.CloseLoadingForm(theradID);
        }

        #region 弹出分发文档界面
        /// <summary>
        /// 窗体按键事件(ESC 关闭窗体)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                PopupWindow mainForm = sender as PopupWindow;
                mainForm.Close();
            }
        }

        #endregion

        #region 弹出修改签收历史比较页面
        /// <summary>
        /// 弹出修改签收历史比较页面
        /// </summary>
        /// <param name="workitem"></param>
        /// <param name="OperationID"></param>
        /// <param name="BeforeApplyID"></param>
        /// <param name="AfterApplyID"></param>
        public static void ShowHistoryReviseBillCompare(WorkItem workitem, Guid OperationID, Guid BeforeApplyID, Guid AfterApplyID)
        {
            int theradID = 0;
            theradID=LoadingServce.ShowLoadingForm("Loading...");
            try
            {
                BillReviseHistoryPart mainSpce = workitem.SmartParts.AddNew<BillReviseHistoryPart>();
                mainSpce.NewOperationID = OperationID;
                mainSpce.BeforeApplyID = BeforeApplyID;
                mainSpce.AfterApplyeID = AfterApplyID;

                IWorkspace mainWorkspace = workitem.Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Revise History Compare" : "修订历史比较";
                mainWorkspace.Show(mainSpce, smartPartInfo);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(LocalData.IsEnglish ? "Revise History Compare is opened,can't open it again" : "修订历史比较已打开，不能再次打开");
            }
            LoadingServce.CloseLoadingForm(theradID);
        }
        #endregion

        /// <summary>
        /// 弹出海出修订签收比较界面
        /// 2013-08-05 joe initial
        /// </summary>
        /// <param name="workitem">WorkItem</param>
        /// <param name="OEOperationID">海出操作ID</param>
        public static void ShowReviseAccepte(WorkItem workitem, Guid OEOperationID)
        {
            int theradID = 0;
            theradID=LoadingServce.ShowLoadingForm("Loading...");
            try
            {
                OEBillRevisePart mainSpce = workitem.SmartParts.AddNew<OEBillRevisePart>();
                mainSpce.NewOperationID = OEOperationID;
                IWorkspace mainWorkspace = workitem.Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Accept Revised D/C Fees From The Agent" : "签收代理修订的费用";
                mainWorkspace.Show(mainSpce, smartPartInfo);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(LocalData.IsEnglish ? "Accept Revised D/C Fees From The Agent is opened,can't open it again" : "签收代理修订的费用已打开，不能再次打开");
            }
            LoadingServce.CloseLoadingForm(theradID);
        }
        #endregion

        #region 批量更新ETA，提货地
        /// <summary>
        /// 批量更新ETA，提货地
        /// </summary>
        /// <param name="workitem"></param>
        /// <param name="context"></param>
        /// <param name="values"></param>
        /// <param name="editPartSaved"></param>
        public static void ShowUpdateETA(WorkItem workitem, UpdateETAInfo context, IDictionary<string, object> values, PartDelegate.EditPartSaved editPartSaved)
        {
            string title = LocalData.IsEnglish ? "Batch Update" : "批量更新";
            PartLoader.ShowEditPart<UpdateETA>(workitem, context, EditMode.Edit, values, title, editPartSaved, "");
        }
        #endregion

        #region 无引用
        #region 处理值类型
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Obsolete("方法已过时")]
        public static bool GuidIsNullOrEmpty(Guid? id)
        {
            if (id == null || id.Value == Guid.Empty) return true;
            else return false;
        }
        /// <summary>
        /// 获取一天最后时间(23:59:59)
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [Obsolete("方法已过时")]
        public static DateTime GetEndDate(DateTime date)
        {
            string dateStr = date.ToShortDateString();
            dateStr += " 23:59:59";
            return DateTime.Parse(dateStr);
        }
        /// <summary>
        /// 根据业务ID实例化BusinessOperationContext
        /// </summary>
        /// <param name="OperationID"></param>
        /// <returns></returns>
        [Obsolete("方法已过时")]
        public static BusinessOperationContext GetBusinessContext(Guid OperationID)
        {
            BusinessOperationContext context = new BusinessOperationContext();
            context.OperationID = OperationID;
            return context;
        }
        #endregion

        /// <summary>
        /// 弹出分发文档签收比较界面
        /// </summary>
        /// <param name="workitem"></param>
        /// <param name="OEOperationID"></param>
        /// <param name="OIOperationID"></param>
        /// <param name="isVisibleAccept"></param>
        [Obsolete("方法已过时")]
        public static void ShowAcceptedDocumentCompare(WorkItem workitem, Guid OEOperationID, Guid OIOperationID, bool isVisibleAccept)
        {
            int theradID = 0;
            theradID = LoadingServce.ShowLoadingForm("Loading...");

            try
            {

                OIDispachCompare mainSpce = workitem.SmartParts.AddNew<OIDispachCompare>();
                mainSpce.NewOperationID = OEOperationID;
                mainSpce.OldOperationID = OIOperationID;
                mainSpce.IsVisibleAccept = isVisibleAccept;


                IWorkspace mainWorkspace = workitem.Workspaces[ClientConstants.MainWorkspace];
                SmartPartInfo smartPartInfo = new SmartPartInfo();
                smartPartInfo.Title = LocalData.IsEnglish ? "Accept Dispatched Docs From The Agent" : "签收代理分发的文档";
                mainWorkspace.Show(mainSpce, smartPartInfo);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(LocalData.IsEnglish ? "Accept Dispatched Docs From The Agent is opened,can't open it again" : "签收代理分发的文档已打开，不能再次打开");
            }
            LoadingServce.CloseLoadingForm(theradID);

        }
        /// <summary>
        /// 弹出分发文档界面
        /// </summary>
        /// <param name="workitem"></param>
        /// <param name="context"></param>
        /// <param name="editPartSaved"></param>
        [Obsolete("方法已过时")]
        public static void ShowDispatchDocument(WorkItem workitem, BusinessOperationContext context, PartDelegate.EditPartSaved editPartSaved)
        {
            //IntPtr agentDispatchMailForm;
            //PopupWindow form = null;
            //FrmSendAgentDocument frmDocument = workitem.SmartParts.AddNew<FrmSendAgentDocument>();
            //frmDocument.DataSource = context;
            //form = new PopupWindow();
            //form.MaximizeBox = form.MinimizeBox = false;
            //form.ShowIcon = true;
            //form.ShowInTaskbar = true;
            //form.KeyPreview = true;
            //form.KeyDown += new KeyEventHandler(form_KeyDown);
            //form.FormBorderStyle = FormBorderStyle.FixedSingle;
            //form.Text = LocalData.IsEnglish ? "Dispatch Document" : "分发文档";
            //form.StartPosition = FormStartPosition.CenterScreen;
            //form.ErrorListControl.Visible = false;
            //frmDocument.Dock = DockStyle.Fill;
            //form.Width = frmDocument.Width + 5;
            //form.Height = frmDocument.Height + 60;
            //form.Controls.Add(frmDocument);
            //agentDispatchMailForm = form.Handle;
            //if (editPartSaved != null)
            //{
            //    frmDocument.Saved += delegate (object[] prams)
            //    {
            //        editPartSaved(prams);
            //    };
            //}
            //form.ShowDialog();
        }
        /// <summary>
        /// 保存事件日志
        /// </summary>
        /// <param name="eventobject"></param>
        [Obsolete("方法已过时")]
        public static void SaveEventInfo(EventObjects eventobject)
        {
            ServiceClient.GetService<IFCMCommonService>().SaveMemoInfo(eventobject);
        }
        #endregion
    }
    /// <summary>
    /// 
    /// </summary>
    public class UIModelHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
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

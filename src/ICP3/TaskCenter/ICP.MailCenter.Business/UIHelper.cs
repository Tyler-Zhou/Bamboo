using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using ICP.Framework.CommonLibrary.Client;
using ICP.Common.ServiceInterface.DataObjects;
using System.Reflection;
using DevExpress.XtraGrid;
using ICP.Framework.CommonLibrary.Helper;
using ICP.MailCenter.ServiceInterface.Common;
using Microsoft.Practices.CompositeUI;

namespace ICP.Operation.Common.ServiceInterface
{
    public static class UIHelper
    {
        /// <summary>
        /// 当前业务面板的类型
        /// </summary>
        public static ListFormType CurrentListFormType;
        /// <summary>
        /// 缓存当前业务面板类型
        /// </summary>
        public static string TemplateCode { get; set; }
        public static ImageList GetImageList(String imageFilePath)
        {
            try
            {
                //D:\ICP3\icpclient\Images\Toolbar
                string[] imageFilePaths = System.IO.Directory.GetFiles(imageFilePath).Where(file => System.Text.RegularExpressions.Regex.IsMatch(file, @"^.+\.(gif|jpg|jpeg|ico|png)$", RegexOptions.IgnoreCase)).ToArray();
                if (imageFilePaths == null || imageFilePaths.Length <= 0)
                    return new ImageList();
                System.Windows.Forms.ImageList list = new ImageList();
                list.ImageSize = new System.Drawing.Size(16, 16);
                foreach (string imageFile in imageFilePaths)
                {
                    string imageName = Path.GetFileName(imageFile);
                    list.Images.Add(imageName, System.Drawing.Image.FromFile(imageFile));
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static Dictionary<string, string> ExtractNoPairs(string refNo, char[] pairSeparator, char keyValueSeparator)
        {
            refNo = refNo ?? string.Empty;
            if (string.IsNullOrEmpty(refNo))
                return new Dictionary<string, string>();
            string[] pairs = refNo.Split(pairSeparator);
            Dictionary<string, string> dicPair = new Dictionary<string, string>();
            for (int i = 0; i < pairs.Length; i++)
            {
                string pair = pairs[i] ?? string.Empty;
                pair = pair.Trim();
                if (string.IsNullOrEmpty(pair))
                    continue;
                string[] keyValue = pair.Split(keyValueSeparator);
                if (keyValue == null || keyValue.Length != 2)
                    continue;
                string key = keyValue[0].Trim();
                string value = keyValue[1].Trim();
                if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value))
                    continue;
                dicPair.Add(key, value);

            }
            return dicPair;

        }
        /// <summary>
        /// 获取行样式
        /// </summary>
        /// <param name="operationNo"></param>
        /// <returns></returns>
        public static StyleFormatCondition GetBusinessStyleFormationCondition(string operationNo, IListBaseBusinessPart basePart)
        {
            StyleFormatCondition rowCondition = new DevExpress.XtraGrid.StyleFormatCondition();
            rowCondition.Tag = "BusinessStyle";
            rowCondition.Appearance.Font = new System.Drawing.Font(basePart.RowFont, System.Drawing.FontStyle.Bold);
            rowCondition.Appearance.Options.UseFont = true;

            rowCondition.Condition = FormatConditionEnum.Expression;
            rowCondition.ApplyToRow = true;  
            rowCondition.Expression = string.Format("[NO] == '{0}'", operationNo);
            return rowCondition;
        }
        /// <summary>
        /// 执行的指定方法
        /// </summary>
        /// <param name="userControl"></param>
        /// <param name="methodName"></param>
        /// <param name="param"></param>
        public static void MethodInvoke(Control control, string methodName, object param)
        {
            MethodInfo method = control.GetType().GetMethod(methodName,
                  BindingFlags.Public
                | BindingFlags.DeclaredOnly
                | BindingFlags.Static
                | BindingFlags.Instance);
            method.Invoke(control, new object[1] { param });
        }
        /// <summary>
        /// 指定属性赋值,取值
        /// </summary>
        /// <param name="control"></param>
        /// <param name="property"></param>
        /// <param name="param"></param>
        public static object PropertyInvoke(Control control, string property, object param, bool isSetPropertyValue)
        {
            PropertyInfo proInfo = control.GetType().GetProperty(property, BindingFlags.Public
                | BindingFlags.SetProperty
                | BindingFlags.GetProperty
                | BindingFlags.Instance);
            if (isSetPropertyValue)
                proInfo.SetValue(control, param, null);
            else
                return proInfo.GetValue(control, null);
            return null;
        }


        public static string regexExpression = "[a-zA-Z0-9]{8,30}";
        public static List<string> MatchArray(string arrText)
        {
            return arrText.MatchUseRegex(regexExpression);
        }

        /// <summary>
        /// 根据全局State的键值来判断是否要去生成列
        /// </summary>
        /// <param name="RootWorkItem"></param>
        /// <returns></returns>
        public static bool IsNeedGenerateColumns(WorkItem RootWorkItem)
        {
            bool bind = false;
            object objBind = RootWorkItem.State["NeedGererateColumns"];
            if (objBind != null && objBind != "")
                bool.TryParse(objBind.ToString(), out bind);
            return bind;
        }

        public static bool IsAdvanceSearch(WorkItem rootWorkItem)
        {
            bool isAdvanceSearch = false;
            object objAdvanceSearch = rootWorkItem.State["IsAdvanceSearch"];
            if (objAdvanceSearch != null && objAdvanceSearch != "")
                bool.TryParse(objAdvanceSearch.ToString(), out isAdvanceSearch);
            return isAdvanceSearch;
        }

        public static string GetXMLParameters(List<BusinessSaveParameter> parameters)
        {
            XDocument document = new XDocument();
            XElement rootElement = new XElement("Data");
            foreach (BusinessSaveParameter parameter in parameters)
            {
                XElement element = new XElement("row");
                foreach (string key in parameter.items.Keys)
                {
                    XAttribute attribute = new XAttribute(key, parameter.items[key]);
                    element.Add(attribute);
                }
                rootElement.Add(element);
            }
            document.Add(rootElement);
            return document.ToString();
        }
    }
    /// <summary>
    /// SO相关设置辅助类
    /// 信息保存到本地配置文件
    /// </summary>
    public class SOSetting
    {
        static SOSetting instance;

        public static SOSetting Current
        {
            get
            {
                if (instance != null)
                {
                    return instance;
                }
                else
                {
                    instance = new SOSetting();
                    return instance;
                }
            }
        }
        private SOSetting()
        {
            this.autoFillSO = GetValue(Constants.AutoFillSOKey);
            this.notfiyCS = GetValue(Constants.NotifyCSKey);
        }
        private bool autoFillSO = true;
        private bool notfiyCS = true;
        /// <summary>
        /// 上传SO附件时是否自动填充SO号
        /// </summary>
        public bool AutoFillSO
        {
            get
            {
                return this.autoFillSO;
            }
            set
            {
                this.autoFillSO = value;
                ClientConfig.Current.AddValue(Constants.AutoFillSOKey, value.ToString());
            }
        }
        /// <summary>
        /// 是否在上传SO时自动通知客服
        /// </summary>
        public bool NotfiyCS
        {
            get
            {
                return this.notfiyCS;
            }
            set
            {
                this.notfiyCS = value;
                ClientConfig.Current.AddValue(Constants.NotifyCSKey, value.ToString());
            }
        }
        private bool GetValue(string key)
        {
            bool result = true;
            if (ClientConfig.Current.Contains(key))
            {
                result = Boolean.Parse(ClientConfig.Current.GetValue(key));
            }
            return result;
        }
    }
}

using ICP.Common.ServiceInterface.DataObjects;
using ICP.EDI.ServiceInterface;
using ICP.EDI.ServiceInterface.DataObjects;
using ICP.Framework.ClientComponents;
using ICP.Framework.ClientComponents.Controls;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace ICP.EDI.UI
{
    public class EDIClientService : IEDIClientService
    {

        #region 服务

        [ServiceDependency]
        public WorkItem WorkItem
        {
            get;
            set;
        }

        /// <summary>
        /// Root WorkItem
        /// </summary>
        public WorkItem RootWorkItem
        {
            get
            {
                return ServiceClient.GetClientService<WorkItem>();
            }
        }

        /// <summary>
        /// EDI服务
        /// </summary>
        public IEDIService EDIService
        {
            get
            {
                return ServiceClient.GetService<IEDIService>();
            }
        }

        #endregion

        /// <summary>
        /// 发送EDI
        /// </summary>
        /// <param name="sendOption">发送选项</param>
        /// <param name="isPreview">是否预览</param>
        /// <returns>是否发送成功</returns>
        public bool ShowForm(EDISendOption sendOption,bool isPreview)
        {
            sendOption.VersionNo = 1;
            EDIConfigItem configItem = EDIService.GetEDIConfigByOption(sendOption);
            if(configItem == null)
            {
                throw new ICPException("没有找到该船东/承运人的EDI服务");
            }
            if (isPreview)
            {
                EDIPreview previewForm = RootWorkItem.Items.AddNew<EDIPreview>();
                string title = LocalData.IsEnglish ? "EDI Data Preview" : "EDI数据预览";
                previewForm.IDs = sendOption.IDs;
                previewForm.EDIType = sendOption.EdiMode;
                DialogResult result = PartLoader.ShowDialog(previewForm, title);
                if (result != DialogResult.OK)
                {
                    return false;
                }
            }
            EDISendForm vForm = WorkItem.Items.AddNew<EDISendForm>();
            vForm.SendOption = sendOption;
            vForm.ConfigOption = configItem;
            DialogResult dlg = vForm.ShowDialog();
            if (dlg == DialogResult.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 通过EDI发送选项获取EDI配置
        /// </summary>
        /// <param name="sendOption">发送选项</param>
        /// <returns></returns>
        public EDIConfigItem GetEDIConfigByOption(EDISendOption sendOption)
        {
            EDIConfigItem configItem = null;
            EDIConfigItem configValue=EDIService.GetEDIConfigByOption(sendOption);
            if(configValue!=null)
            {
                configItem = new EDIConfigItem()
                {
                    ID = configValue.ID,
                    Code = configValue.Code,
                    SubjectPrefix = configValue.SubjectPrefix,
                    EDIMode = configValue.EDIMode,
                };
            }
            return configItem;
        }

        #region 发送EDI
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serverConfigKey">key,项目名称，例HANJIN.Booking</param>
        /// <param name="ediMode">类型，订舱/补料/AMS……</param>
        /// <param name="companyID">船公司ID</param>
        /// <param name="subject">主题</param>
        /// <param name="oids">OceanBookingID</param>
        /// <param name="fIds">MBLID或HBLID</param>
        /// <param name="nos">对应的（MBLNO/HBLNO）单号</param>
        /// <param name="jobType"></param>
        /// <returns></returns>
        [Obsolete("方法已过时，请使用 ShowForm(EDISendOption sendOption, bool isPreview) 代替")]
        public bool SendEDI(EDISendOption sendItem)
        {
            string serverConfigKey = sendItem.ServiceKey;
            EDIMode ediMode = sendItem.EdiMode;
            Guid companyID = sendItem.CompanyID;
            string subject = sendItem.Subject;
            Guid[] oids = sendItem.IDs;
            Guid[] fIds = sendItem.FIDs;
            string[] nos = sendItem.NOs;
            OperationType jobType = sendItem.OperationType;
            AMSEntryType amsEntryType = sendItem.AMSEntryType;
            ACIEntryType aciEntryType = sendItem.ACIEntryType;


            EDISendForm vForm = WorkItem.Items.AddNew<EDISendForm>();
            vForm.SetData(serverConfigKey, ediMode, companyID, subject, oids, fIds, nos, jobType, amsEntryType, aciEntryType);
            vForm.shipperFormat = sendItem.ShipperFormat;
            vForm.shipperName = sendItem.ShipperName;
            vForm.consigneeFormat = sendItem.ConsigneeFormat;
            vForm.consigneeName = sendItem.ConsigneeName;
            vForm.NotifyFormat = sendItem.NotifyFormat;
            vForm.NotifyName = sendItem.NotifyName;
            vForm.otherFormat = sendItem.OtherFormat;
            vForm.CarrierID = sendItem.CarrierID;
            vForm.goodinfoFormat = sendItem.GoodinfoFormat;
            vForm.markFormat = sendItem.MarkFormat;

            DialogResult dlg = vForm.ShowDialog();
            if (dlg == DialogResult.OK)
            {
                //写入日志
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region 格式化字符串
        private string XMLDecode(string sText)
        {
            sText = sText.Replace("&amp;", "&");
            sText = sText.Replace("&lt;", "<");
            sText = sText.Replace("&gt;", ">");
            sText = sText.Replace("&apos;", "'");
            sText = sText.Replace("&quot;", "\"");
            return sText;
        }

        private string XMLEncode(string sText)
        {
            sText = sText.Replace("&", "&amp;");
            sText = sText.Replace("<", "&lt;");
            sText = sText.Replace(">", "&gt;");
            sText = sText.Replace("'", "&apos;");
            sText = sText.Replace("\"", "&quot;");
            return sText;
        }

        private string[] SplitStringToWord(string val, bool isisRemoveBlankLines)
        {
            if (string.IsNullOrEmpty(val) == true) return new string[] { };
            //String regex = "([\\w]*(\\'|\\-)*[\\w]*)|([\\w]+)|([-,，?？.:;·。；：~￥%…()|+{}$&*%#@=>/\\<!]+)|([\\s]*)|([\\r\\n]*)";
            //String regex = "(&amp;|&quot;|&apos;|&lt;|&gt)|(\\'|\\-)|([\\w]+(\\'|\\-|&amp;|&quot;|&apos;|&lt;|&gt)*[\\w]+)|([\\w]+)|([,?.:;，。？；：(){} $&*%#@=+>/\\<!]+)|([\\-]*)|([\\s]*)";
            //String regex = "(\\'|\\-|\r|\n|\r\n)|([\\w]+(\\'|\\-)*[\\w]+)|([\\w]+)|([\",?.:;，。？；：(){} $&*%#@=+>/\\<!]+)|([\\-]*)|([\\s]*)";
            //String regex = "(\\'|\\-|\r\n)|([\\w]+(\\'|\\-)*[\\w]+)|([\\w]+)|([\",?.:;，。？；：(){} $&*%#@=+>/\\<!]+)|([\\-]*)|([\\s]*)";

            String regex = string.Empty;
            if (isisRemoveBlankLines)
            {
                regex = "(\\'|\\-|\r|\n|\r\n)|([\\w]+(\\'|\\-)*[\\w]+)|([\\w]+)|([\",?.:;，。？；：(){} $&*%#@=+>/\\<!]+)|([\\-]*)|([\\s]*)";
            }
            else
            {
                regex = "(\\'|\\-|\r\n)|([\\w]+(\\'|\\-)*[\\w]+)|([\\w]+)|([\",?.:;，。？；：(){} $&*%#@=+>/\\<!]+)|([\\-]*)|([\\s]*)";
            }



            Regex pattern = new Regex(regex, RegexOptions.Compiled);
            MatchCollection cs = pattern.Matches(val);
            List<string> vals = new List<string>();
            foreach (Match m in cs)
            {
                //if (m.Value == "\r" || m.Value == "\n")
                //{
                //    vals.Add("\r\n");
                //}
                //else
                //{
                vals.Add(m.Value);
                //}
            }

            return vals.ToArray();
        }

        private bool IsEnterOrLine(string val)
        {
            if (string.IsNullOrEmpty(val)) return false;

            bool isTrue = false;
            char[] cs = val.ToCharArray();
            foreach (char c in cs)
            {
                int code = (int)c;
                if (code == 10 || code == 13)
                {
                    isTrue = true;

                    break;
                }
            }

            return isTrue;
        }

        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <param name="val">文本内容</param>
        /// <param name="rowLength">行长度</param>
        /// <param name="maxRow">最大行数</param>
        /// <returns></returns>
        public string SplitString(string val, int rowLength, int maxRow)
        {
            return SplitString(val, rowLength, maxRow, true);
        }


        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <param name="val">文本内容</param>
        /// <param name="rowLength">行长度</param>
        /// <param name="maxRow">最大行数</param>
        /// <param name="isRemoveBlankLines">是否需要去空行</param>
        /// <returns></returns>
        public string SplitString(string val, int rowLength, int maxRow, bool isRemoveBlankLines)
        {
            if (maxRow == 0) maxRow = 999;

            List<string> total = new List<string>();
            StringBuilder orgsb = new StringBuilder();
            int orgsbLength = 0;
            string[] words = SplitStringToWord(val, isRemoveBlankLines);
            foreach (string w in words)
            {
                if (IsEnterOrLine(w))
                {
                    // orgsb.Append(Environment.NewLine);
                    total.Add(orgsb.ToString());
                    orgsb = new StringBuilder();
                    orgsbLength = 0;
                }

                int wordLength = w.Length;
                if (orgsbLength + wordLength > rowLength)
                {
                    if (total.Count < maxRow)
                    {
                        total.Add(orgsb.ToString());
                    }

                    orgsb = new StringBuilder();
                    orgsbLength = 0;
                }
                if (IsEnterOrLine(w) == false)
                {
                    orgsb.Append(w);
                    orgsbLength += wordLength;
                }

                if (w.Contains("\r\n\r\n"))
                {
                    orgsb.AppendLine();
                }

            }

            if (total.Count < maxRow && orgsb.Length > 0)
            {
                total.Add(orgsb.ToString());
            }

            StringBuilder results = new StringBuilder();
            #region 去除空行

            foreach (var item in total)
            {
                if (isRemoveBlankLines)
                {
                    if (string.IsNullOrEmpty(item.Trim())) continue;
                }
                if (results.Length > 0) results.AppendLine();

                results.Append(item.Trim());
            }
            #endregion

            return results.ToString();
        }
        #endregion


    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ICP.Operation.Common.ServiceInterface
{
    /// <summary>
    /// 样式设计
    /// </summary>
    public static class RowStyleConfigController
    {


        //读取模版的路径
        private static readonly string fileRootDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BusinessTemplates");
        //Xml的文件名称
        private const string TempalteFileName = "RowStyle.xml";

        private static List<RowStyleConfigInfo> rowStyleConfigInfos = new List<RowStyleConfigInfo>();

        /// <summary>
        /// 返回XML对象
        /// </summary>
        /// <returns></returns>
        public static List<RowStyleConfigInfo> GetXmList()
        {
            //合并为完整的XML路径
            var templateFilePath = Path.Combine(fileRootDirectory, TempalteFileName);
            var document = XDocument.Load(templateFilePath);
            //读取Item下面的所有集合信息
            var xmldocment = from ent in document.Descendants("Items") select ent;
            //读取Item段落的信息
            var xmlitem = from item in xmldocment.Descendants(XName.Get("Item"))
                          select item;
            foreach (var x in xmlitem)
            {

                RowStyleConfigInfo rowStyleConfigInfo = new RowStyleConfigInfo();
                rowStyleConfigInfo.TemplateCode = x.Attribute("TemplateCode").Value;
                rowStyleConfigInfo.RowStyle = bool.Parse(x.Attribute("RowStyle").Value);
                rowStyleConfigInfo.JudgeConditions = x.Attribute("JudgeConditions").Value;
                rowStyleConfigInfo.TrueColor = Color.FromName(x.Attribute("TrueColor").Value);
                rowStyleConfigInfo.FalseColor = Color.FromName(x.Attribute("FalseColor").Value);
                rowStyleConfigInfo.Field = x.Attribute("Field").Value;
                rowStyleConfigInfo.ToolTipCn = x.Attribute("ToolTipCn").Value;
                rowStyleConfigInfo.ToolTipEn = x.Attribute("ToolTipEn").Value;
                rowStyleConfigInfo.Font = bool.Parse(x.Attribute("Font").Value);
                rowStyleConfigInfos.Add(rowStyleConfigInfo);
            }
            return rowStyleConfigInfos;
        }

        /// <summary>
        /// 返回列表样式条件
        /// </summary>
        /// <param name="templateCode">节点Code</param>
        /// <returns></returns>
        public static List<RowStyleConfigInfo> GetRowStyleName(string templateCode)
        {
            List<RowStyleConfigInfo> rowStyleConfig = new List<RowStyleConfigInfo>();
            if (rowStyleConfigInfos.Count == 0)
            {
                rowStyleConfigInfos = GetXmList();
            }
            string str = string.Empty;
            List<RowStyleConfigInfo> infos = rowStyleConfigInfos.Where(n => n.RowStyle).ToList();
            foreach (var row in infos)
            {
                if (!string.IsNullOrEmpty(row.TemplateCode))
                {
                    string[] TemplateCode = row.TemplateCode.Split(',');
                    for (int i = 0; i < TemplateCode.Count(); i++)
                    {
                        if (TemplateCode[i] == templateCode)
                        {
                            rowStyleConfig.Add(row);
                        }
                    }
                }
            }
            return rowStyleConfig;
        }

        /// <summary>
        /// 返回单元格样式
        /// </summary>
        /// <param name="templateCode">节点Code</param>
        /// <returns></returns>
        public static List<RowStyleConfigInfo> GetRowCellStyleName(string templateCode)
        {
            List<RowStyleConfigInfo> rowStyleConfig = new List<RowStyleConfigInfo>();
            if (rowStyleConfigInfos.Count == 0)
            {
                rowStyleConfigInfos = GetXmList();
            } List<RowStyleConfigInfo> infos = rowStyleConfigInfos.Where(n => n.RowStyle == false).ToList();
            foreach (RowStyleConfigInfo row in infos)
            {
                if (!string.IsNullOrEmpty(row.TemplateCode))
                {
                    string[] TemplateCode = row.TemplateCode.Split(',');
                    for (int i = 0; i < TemplateCode.Count(); i++)
                    {
                        if (TemplateCode[i] == templateCode)
                        {
                            rowStyleConfig.Add(row);
                        }
                    }
                }
            }
            return rowStyleConfig;
        }
    }
}

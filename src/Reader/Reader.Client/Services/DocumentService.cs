using Reader.Client.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static ImTools.ImMap;

namespace Reader.Client.Services
{
    /// <summary>
    /// 文档服务
    /// </summary>
    public class DocumentService
    {
        /// <summary>
        /// 保存路径
        /// </summary>
        string _SavePath { get; set; }

        /// <summary>
        /// 文档服务
        /// </summary>
        /// <param name="savePath"></param>
        public DocumentService(string savePath)
        {
            _SavePath = savePath;
        }

        /// <summary>
        /// 样式表
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="CSSStyles">样式集合</param>
        public void StyleSheet(string fileName, IEnumerable<BaseDataModel> CSSStyles)
        {
            EnsureDirectoryExists(_SavePath);
            string fullPath = $"{_SavePath}{fileName}.css";
            if(File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
            StringBuilder stringBuilder = new StringBuilder();
            foreach (BaseDataModel item in CSSStyles)
            {
                stringBuilder.AppendLine($"{item.Description}");
            }
            WriteFile(fullPath, stringBuilder.ToString());
        }

        /// <summary>
        /// 写入HTML页面
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="model">模板模型</param>
        /// <param name="ReplaceRules">替换规则</param>
        /// <param name="dicModel">模型字典</param>
        public void HtmlPage(string fileName,BaseDataModel model, IEnumerable<BaseDataModel> ReplaceRules, Dictionary<string,object> dicModel)
        {
            EnsureDirectoryExists(_SavePath);
            string fullPath = $"{_SavePath}{fileName}.xhtml";
            string htmlContent = model.Description;

            Regex regex = new Regex("\\$(?<R>[\\w\\.]*)\\$");
            MatchCollection matchs= regex.Matches(model.Description);
            foreach (Match match in matchs)
            {
                GroupCollection groups = match.Groups;
                foreach (Group group in groups)
                {
                    if("R".Equals(group.Name))
                    {
                        string matchString = group.Value;
                        string[] replachars = matchString.Split('.');
                        if (replachars.Length < 2)
                            continue;
                        object obj = dicModel[replachars[0]];
                        Type typeObject = obj.GetType();
                        PropertyInfo propertyInfo = typeObject.GetProperty(replachars[1]);
                        if(propertyInfo==null)
                            continue;
                        object objValue = propertyInfo.GetValue(obj, null);
                        if (objValue == null)
                            continue;
                        htmlContent = htmlContent.Replace($"${matchString}$", $"{objValue}");
                    }
                }
            }
            //替换影响呈现内容
            foreach (BaseDataModel item in ReplaceRules)
            {
                htmlContent = htmlContent.Replace(item.Name, item.Description);
            }
            WriteFile(fullPath, htmlContent);
        }

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="fileContent"></param>
        void WriteFile(string fullPath,string fileContent)
        {
            //写入文件流
            using (StreamWriter sw = new StreamWriter(fullPath))
            {
                sw.WriteLine(fileContent);
            }
        }

        /// <summary>
        /// 确保目录存在
        /// </summary>
        /// <param name="dir"></param>
        void EnsureDirectoryExists(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }
    }
}

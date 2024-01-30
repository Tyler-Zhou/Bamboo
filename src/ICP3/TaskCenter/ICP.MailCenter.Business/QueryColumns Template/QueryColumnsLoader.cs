using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.IO;

namespace ICP.Common.Business.ServiceInterface
{
    /// <summary>
    /// 通过QueryColomnConfig.xml配置得到查询条件列名
    /// </summary>
    public  class QueryColumnLoader
    {

        private readonly static string fileRootDirectory = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "BusinessTemplates");
        private readonly static string tempalteFileName = "QueryColomnConfig.xml";
       static string  fileFullPath;

        private    static string GetFileFullPath()
        {
            if (!string.IsNullOrEmpty(fileFullPath))
                return fileFullPath;
            if (!Directory.Exists(fileRootDirectory))
                Directory.CreateDirectory(fileRootDirectory);
            fileFullPath = Path.Combine(fileRootDirectory, tempalteFileName);
            return fileFullPath;
        }
        /// <summary>
        ///  通过QueryColomnConfig.xml配置得到查询条件列名
        /// </summary>
        /// <param name="opereateViewCode">操作视图编码</param>
        /// <returns>条件列名列表</returns>
        public static List<string> GetQueryColumns(string opereateViewCode)
        {

            List<string> lstResult = new List<string>();
            XDocument document = null;
            try
            {

                string templateFilePath = GetFileFullPath();
                document = XDocument.Load(templateFilePath);
                var sectionElements = from item in document.Element(XName.Get("Config")).Element(XName.Get(opereateViewCode)).
                                          Element(XName.Get("Items")).Elements()
                                      select item;
                foreach (XElement xe in sectionElements)
                {
                    lstResult.Add(xe.Attribute("Id").Value);
                }
            }
            catch (System.Exception ex)
            {
                return lstResult;
            }
            finally
            {
                document = null;
            }
            return lstResult;
        }
    }
}

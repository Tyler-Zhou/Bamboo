using ICP.MailCenter.ServiceInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ICP.Operation.Common.ServiceInterface
{  
    /// <summary>
    /// 列模板文件信息类
    /// </summary>
   public class ColumnTemplateFileData:TemplateFileData
    {
        private string fileRootDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BusinessTemplates");
        private string tempalteFileName = "ColumnTemplate.xml";
        string fileFullPath;

        public override string GetFileFullPath()
        {
            
            if (!Directory.Exists(fileRootDirectory))
                Directory.CreateDirectory(fileRootDirectory);
            fileFullPath = Path.Combine(fileRootDirectory, tempalteFileName);
            return fileFullPath;
        }
        private object synObj = new object();
        public override void ReadSections()
        {
           
                string templateFilePath = GetFileFullPath();
                XDocument document = XDocument.Load(templateFilePath);
                var sectionElements = document.Element(XName.Get("Template")).Elements().ToList();
                if (sectionElements.Count <= 0)
                    return;
                List<string> sectionNames = (from element in sectionElements
                                             select element.Name.LocalName).ToList();

                int count = sectionNames.Count;
                for (int i = 0; i < count; i++)
                {
                    Add(sectionNames[i], new ColumnTemplateSectionData { Name = sectionNames[i], FilePath = fileFullPath });
                }
                document = null;
            
        }
    }
}

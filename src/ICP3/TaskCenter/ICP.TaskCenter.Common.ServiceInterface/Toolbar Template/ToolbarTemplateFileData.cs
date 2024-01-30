using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.MailCenter.ServiceInterface;
using System.IO;
using System.Xml.Linq;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.MailCenter.Business.ServiceInterface
{
   public class ToolbarTemplateFileData:TemplateFileData
    {
        
        private string fileRootDirectory = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "BusinessTemplates");
        private string tempalteFileName = "ToolbarConfig.xml";
        string fileFullPath;

        public override string GetFileFullPath()
        {
            if (!string.IsNullOrEmpty(fileFullPath))
                return fileFullPath;
            if (!Directory.Exists(fileRootDirectory))
                Directory.CreateDirectory(fileRootDirectory);
            fileFullPath = Path.Combine(fileRootDirectory, tempalteFileName);
            return fileFullPath;
        }

        public override void ReadSections()
        {
           
                string templateFilePath = GetFileFullPath();
                XDocument document = XDocument.Load(templateFilePath);
                var sectionElements = document.Element(XName.Get("Config")).Elements().ToList();
                if (sectionElements.Count <= 0)
                    return;
                List<string> sectionNames = (from element in sectionElements
                                             select element.Name.LocalName).ToList();

                int count = sectionNames.Count;
                for (int i = 0; i < count; i++)
                {
                    Add(sectionNames[i], new ToolbarTemplateSectionData { Name = sectionNames[i], FilePath = this.fileFullPath });
                }
                document = null;
            
        
        }
    }
}

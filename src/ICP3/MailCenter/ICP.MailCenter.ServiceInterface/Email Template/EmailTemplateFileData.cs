using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Linq;

namespace ICP.MailCenter.ServiceInterface
{   
    /// <summary>
    /// Email模板文件字典类
    /// </summary>
    public class EmailTemplateFileData : TemplateFileData
    {
        private string languageName = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
        
        private string fileRootDirectory = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "EmailTemplate");
        private string fileFullPath;
        private string extensionName = ".etl";
       
        /// <summary>
        /// 文件对应的语言
        /// </summary>
        public string LanguageName
        {
            get
            {
                return this.languageName;
            }
            set
            {

                this.languageName = value;
            }

        }
        /// <summary>
        /// 文件根路径
        /// </summary>
        public string FileRootPath
        {
            get
            {
                return this.fileRootDirectory;
            }
            set
            {
                this.fileRootDirectory = value;
            }

        }
        public override string GetFileFullPath()
        {
            string rootPath = FileRootPath;
            if (!Directory.Exists(rootPath))
                Directory.CreateDirectory(rootPath);
            fileFullPath = Path.Combine(FileRootPath, LanguageName + extensionName);
            return fileFullPath;
        }

        public override void ReadSections()
        {
            XDocument document = XDocument.Load(this.fileFullPath, LoadOptions.PreserveWhitespace);
            var sectionElements = document.Element(XName.Get("Language")).Elements().ToList();
            if (sectionElements.Count <= 0)
                return;
            List<string> sectionNames = (from element in sectionElements
                                         select element.Name.LocalName).ToList();
            int count = sectionNames.Count;
            for (int i = 0; i < count; i++)
            {
                Add(sectionNames[i], new EmailTemplateSectionData { Name = sectionNames[i], FilePath = this.fileFullPath });
            }
            document = null;
        }
    }
}

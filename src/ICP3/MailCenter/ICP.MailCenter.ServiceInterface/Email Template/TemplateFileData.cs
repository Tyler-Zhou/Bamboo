using System.IO;

namespace ICP.MailCenter.ServiceInterface
{
    /// <summary>
    /// 模板文件字典类
    /// </summary>
    public abstract class TemplateFileData : TemplateBaseDictionary<string,TemplateSectionData> 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract string GetFileFullPath();
        /// <summary>
        /// 
        /// </summary>
        public abstract void ReadSections();
        /// <summary>
        /// 
        /// </summary>
        public  void Init()
        {
            string filePath = GetFileFullPath();
            if (!File.Exists(filePath))
            {
                File.Create(filePath);

            }
            ReadSections();

        }
        


    }
}

using System.Collections.Generic;
using System.Xml.Linq;

namespace ICP.MailCenter.ServiceInterface
{
    /// <summary>
    /// 模板段落类
    /// </summary>
    public abstract class TemplateSectionData : TemplateBaseDictionary<string, ITemplateItemData> 
    {
        private string name;
        private string filePath;
        private object synObj = new object();
        /// <summary>
        /// 段落名称
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        /// <summary>
        /// 模板文件绝对路径
        /// </summary>
        public string FilePath
        {
            get { return this.filePath; }
            set { this.filePath = value; }
        }
        public XDocument Document
        {
            get;
            private set;
        }

        public override void EnsureExists(string itemKey)
        {

            if (ContainsKey(itemKey))
                return;
            Document = XDocument.Load(this.FilePath, LoadOptions.PreserveWhitespace);
            var itemElements = GetChildElements();
            if (itemElements.Count <= 0)
                return;

            foreach (var element in itemElements)
            {
                ITemplateItemData item = GetTemplateItemData();
                Add(GetKeyName(element), item.Init(element));
            }

            Document = null;

        }
        /// <summary>
        /// 获取子项实例
        /// </summary>
        /// <returns></returns>
        public abstract ITemplateItemData GetTemplateItemData();
        /// <summary>
        /// 获取键名
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public abstract string GetKeyName(XElement element);
        /// <summary>
        /// 子项xml节点
        /// </summary>
        /// <returns></returns>
        public abstract List<XElement> GetChildElements();

    }
}

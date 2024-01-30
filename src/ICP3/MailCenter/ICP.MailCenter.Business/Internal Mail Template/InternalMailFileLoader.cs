using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using ICP.Framework.CommonLibrary.Common;


namespace ICP.MailCenter.Business.ServiceInterface
{
    /// <summary>
    /// 内部邮件链接业务面板载入类
    /// </summary>
    public class InternalMailFileLoader : IDisposable
    {
        #region 构建实例
        static InternalMailFileLoader()
        {
            Current = new InternalMailFileLoader();
        }
        public static InternalMailFileLoader Current { get; set; }
        #endregion

        private string _interMailPath = string.Empty;
        private string InterMailPath
        {
            get
            {
                if (_interMailPath == string.Empty)
                {
                    _interMailPath = string.Format(@"{0}\BusinessTemplates\{1}", AppDomain.CurrentDomain.BaseDirectory, "MailBusinessPartTemplate.xml");
                }

                return _interMailPath;
            }
        }

        /// <summary>
        /// 内部邮件模版解析存放的集合
        /// </summary>
        private Dictionary<string, MailBusinessPartTemplateData> _DicMailTemplateData;
        public Dictionary<string, MailBusinessPartTemplateData> dicMailTemplateData
        {
            get
            {
                return _DicMailTemplateData ?? (_DicMailTemplateData = new Dictionary<string, MailBusinessPartTemplateData>());
            }
            set { _DicMailTemplateData = value; }
        }

        private OperationType OperationType { get; set; }
        private string TemplateCode { get; set; }


        private IEnumerable<XElement> _Elements;
        public IEnumerable<XElement> Elements
        {
            get
            {
                XDocument document = XDocument.Load(InterMailPath);
                //_Elements =
                //    document.Elements(XName.Get(OperationType.ToString()))
                //            .Descendants(XName.Get("Actions"))
                //            .Elements(TemplateCode);

                _Elements = document.Element(XName.Get("Searcher")).Elements(XName.Get("Actions"))
                            .Descendants(XName.Get(OperationType.ToString()))
                            .Elements(TemplateCode);

                return _Elements;
            }
        }

        public MailBusinessPartTemplateData this[SelectionKey item]
        {
            get { return GetInternalMailItems(item); }
        }

        public MailBusinessPartTemplateData GetInternalMailItems(SelectionKey selection)
        {
            this.OperationType = selection.Type;
            this.TemplateCode = selection.TemplateCode;

            if (dicMailTemplateData.ContainsKey(selection.ToString()))
                return dicMailTemplateData[selection.ToString()];

            MailBusinessPartTemplateData item = GetItem();
            AddItemToDictionaryList(selection.ToString(), item);
            return item;
        }

        public MailBusinessPartTemplateData GetItem()
        {
            var item = new MailBusinessPartTemplateData();
            if (Elements.Count() <= 0)
                return item;

            foreach (var element in Elements)
            {
                item.Init(element);
            }
            return item;
        }

        private void AddItemToDictionaryList(string key, MailBusinessPartTemplateData value)
        {
            if (!dicMailTemplateData.ContainsKey(key))
            {
                dicMailTemplateData.Add(key, value);
            }
        }

        #region IDisposable 成员

        public void Dispose()
        {
            dicMailTemplateData.Clear();
        }

        #endregion
    }
    /// <summary>
    /// 内部邮件链接面板键值实体类
    /// </summary>
    public sealed class SelectionKey
    {
        public string TemplateCode { get; set; }

        public OperationType Type { get; set; }

        public override string ToString()
        {
            return string.Format("{0}&&{1}", this.TemplateCode, this.Type.ToString());
        }
    }
}

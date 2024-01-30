using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ICP.MailCenter.Business.ServiceInterface
{
    /// <summary>
    /// 业务面板数据查询加载类
    /// </summary>
    public class QueryStringFileLoder : IDisposable
    {
        #region 构建实例
        static QueryStringFileLoder()
        {
            Current = new QueryStringFileLoder();
        }
        public static QueryStringFileLoder Current { get; set; }
        #endregion

        private string _interMailPath = string.Empty;
        private string InterMailPath
        {
            get
            {
                if (_interMailPath == string.Empty)
                {
                    _interMailPath = string.Format(@"{0}\BusinessTemplates\{1}", AppDomain.CurrentDomain.BaseDirectory, "BusinessQueryString.xml");
                }

                return _interMailPath;
            }
        }

        /// <summary>
        /// 查询语句集合
        /// </summary>
        private Dictionary<string, QueryStringTemplateData> _DicQueryStrings;
        public Dictionary<string, QueryStringTemplateData> dicQueryStrings
        {
            get
            {
                return _DicQueryStrings ?? (_DicQueryStrings = new Dictionary<string, QueryStringTemplateData>());
            }
            set { _DicQueryStrings = value; }
        }


        private IEnumerable<XElement> _Elements;
        public IEnumerable<XElement> Elements
        {
            get
            {
                XDocument document = XDocument.Load(InterMailPath);
                _Elements =
                    document.Elements(XName.Get("Configs")).Elements(TemplateCode);
                return _Elements;
            }
        }

        public string TemplateCode { get; set; }

        public QueryStringTemplateData this[string templateCode]
        {
            get { return GetInternalMailItems(templateCode); }
        }

        public QueryStringTemplateData GetInternalMailItems(string templateCode)
        {
            this.TemplateCode = templateCode;
            if (dicQueryStrings.ContainsKey(templateCode))
                return dicQueryStrings[templateCode];

            QueryStringTemplateData item = GetItem();
            if (!dicQueryStrings.ContainsKey(templateCode))
                dicQueryStrings.Add(templateCode, item);
            return item;
        }

        public QueryStringTemplateData GetItem()
        {
            var item = new QueryStringTemplateData();
            if (Elements.Count() <= 0)
                return item;
            foreach (var element in Elements)
            {
                item.Init(element);
            }
            return item;
        }



        #region IDisposable 成员

        public void Dispose()
        {
            dicQueryStrings.Clear();
            dicQueryStrings = null;
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.MailCenter.ServiceInterface;

namespace ICP.MailCenter.Business.ServiceInterface
{
    /// <summary>
    /// 模板载入基类
    /// </summary>
    /// <typeparam name="F"></typeparam>
    /// <typeparam name="S"></typeparam>
    /// <typeparam name="I"></typeparam>
    public class BusinessTemplateBaseLoader<F, S, I>
        where F : TemplateFileData, new()
        where S : TemplateSectionData
        where I : class, ITemplateItemData
    {
        private static F templateData;
        private static object synObj = new Object();
        private static BusinessTemplateBaseLoader<F, S, I> loader;
        private static Dictionary<string, List<I>> dicItemInfos = new Dictionary<string, List<I>>(new SectionNameEqualityComparer());

        public static BusinessTemplateBaseLoader<F, S, I> Current
        {
            get
            {
                if (loader == null)
                {
                    lock (synObj.GetType())
                    {
                        if (loader == null)
                        {
                            loader = new BusinessTemplateBaseLoader<F, S, I>();
                        }
                    }
                }
                return loader;

            }
        }
        public void SetFilePath(string templateFilePath)
        {

        }
        /// <summary>
        /// 根据列表模板代码返回列表对应的子项信息集合
        /// </summary>
        /// <param name="templateCode"></param>
        /// <returns></returns>
        public List<I> this[string templateCode]
        {
            get
            {
                if (templateData == null)
                {
                    templateData = new F();
                    templateData.Init();
                }

                if (dicItemInfos.ContainsKey(templateCode))
                    return dicItemInfos[templateCode];
                S sectionData = templateData[templateCode] as S;
                ITemplateItemData stub = sectionData[templateCode];
                List<I> itemInfos = new List<I>();
                foreach (var column in sectionData.Values)
                {
                    itemInfos.Add(column as I);
                }
                dicItemInfos.Add(templateCode, itemInfos);
                return itemInfos;


            }

        }
    }
    public class SectionNameEqualityComparer : IEqualityComparer<string>
    {

        #region IEqualityComparer<string> 成员

        public bool Equals(string x, string y)
        {
            return x.ToLower().Equals(y.ToLower());
        }

        public int GetHashCode(string obj)
        {
            return obj.GetHashCode();
        }

        #endregion
    }
}

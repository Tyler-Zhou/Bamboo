using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Linq;
using Microsoft.Practices.CompositeUI;

namespace ICP.Operation.Common.ServiceInterface
{
    /// <summary>
    /// 业务信息抽取器工厂
    /// </summary>
    public class BusinessInfoExtractorFactory
    {
        [ServiceDependency]
        public WorkItem WorkItem { get; set; }
        private static Dictionary<string, IBusinessInfoExtractor> dicExtractor = new Dictionary<string, IBusinessInfoExtractor>();
        private static Dictionary<string, ICustomColumnGetter> dicGetter = new Dictionary<string, ICustomColumnGetter>();
        private static Dictionary<string, IQueryCriteriaGetter> dicQueryCriteriaGetter = new Dictionary<string, IQueryCriteriaGetter>();
        private static Dictionary<string, IBusinessQueryServiceGetter> dicQueryServiceGetter = new Dictionary<string, IBusinessQueryServiceGetter>();
        private static Dictionary<string, IDataBinder> dicDataBinder = new Dictionary<string, IDataBinder>();
        private static Dictionary<string, IPostDataBindHandler> dicPostHandler = new Dictionary<string, IPostDataBindHandler>();
        private static Dictionary<string, IBusinessSpecialConditioner> dicSpecialConditioner = new Dictionary<string, IBusinessSpecialConditioner>();
        private static List<ExtractorItemInfo> extractorItems = null;
        /// <summary>
        /// 获取解析显示面板参数的解析器
        /// </summary>
        /// <param name="parameterType"></param>
        /// <returns></returns>
        public IBusinessInfoExtractor GetExtractor(string typeFullName)
        {
            ExtractorItemInfo item = GetExtractorItem(typeFullName);
            if (item == null) return null;
            return InnerGetter<IBusinessInfoExtractor>(typeFullName, item.ExtractorType, dicExtractor);

        }
        /// <summary>
        /// 获取数据绑定器
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public IDataBinder GetDataBinder(string typeFullName)
        {
            ExtractorItemInfo item = GetExtractorItem(typeFullName);
            return InnerGetter<IDataBinder>(typeFullName, item.DataBinderType, dicDataBinder);
        }
        public IBusinessQueryServiceGetter GetQueryService(string typeFullName)
        {
            ExtractorItemInfo item = GetExtractorItem(typeFullName);
            return InnerGetter<IBusinessQueryServiceGetter>(typeFullName, item.QueryServiceGetterType, dicQueryServiceGetter);

        }
        private T InnerGetWithDefaultReturn<T, D>(string typeFullName, string typeName, Dictionary<string, T> dic) where D : T, new()
        {
            if (dic.ContainsKey(typeFullName))
                return dic[typeFullName];
            else
            {
                ExtractorItemInfo item = GetExtractorItem(typeFullName);
                if (string.IsNullOrEmpty(typeName))
                {
                    D defaultHandler = new D();
                    dic.Add(typeFullName, defaultHandler);
                    return defaultHandler;
                }
                return InnerGetter<T>(typeFullName, typeName, dic);
            }
        }
        public IPostDataBindHandler GetPostHandler(string typeFullName)
        {
            ExtractorItemInfo item = GetExtractorItem(typeFullName);
            return InnerGetWithDefaultReturn<IPostDataBindHandler, DefaultPostDataBindHandler>(typeFullName, item.PostHandlerType, dicPostHandler);

        }
        private string getFullTypeName(object parameter)
        {
            return parameter.GetType().FullName;
        }
        /// <summary>
        /// 获取查询条件获取器
        /// </summary>
        /// <param name="parameterType"></param>
        /// <returns></returns>
        public IQueryCriteriaGetter GetQueryCriteriaGetter(string typeFullName)
        {
            ExtractorItemInfo item = GetExtractorItem(typeFullName);
            return InnerGetter<IQueryCriteriaGetter>(typeFullName, item.QueryCriteriaGetterType, dicQueryCriteriaGetter);

        }
        /// <summary>
        /// 特殊业务条件处理
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public IBusinessSpecialConditioner GetSpeciallyConditioner(string typeFullName)
        {
            ExtractorItemInfo item = GetExtractorItem(typeFullName);
            return InnerGetWithDefaultReturn<IBusinessSpecialConditioner, DefaultBusinessSpecialConditioner>(typeFullName, item.SpecialConditionerType, dicSpecialConditioner);
        }

        private T InnerGetter<T>(string parameterType, string typeName, Dictionary<string, T> dic)
        {
            if (dic.ContainsKey(parameterType))
                return dic[parameterType];
            else
            {
                ExtractorItemInfo item = GetExtractorItem(parameterType);
                Type type = Type.GetType(typeName, true, true);
                T getter = (T)WorkItem.Items.AddNew(type);
                dic.Add(parameterType, getter);
                return getter;

            }
        }
        /// <summary>
        /// 获取用户自定义数据源列生成器
        /// </summary>
        /// <param name="parameterType"></param>
        /// <returns></returns>
        public ICustomColumnGetter GetCustomColumnGetter(string typeFullName)
        {
            ExtractorItemInfo item = GetExtractorItem(typeFullName);
            return InnerGetWithDefaultReturn<ICustomColumnGetter, DefaultCustomColumnGetter>(typeFullName, item.CustomColumnGetterType, dicGetter);
        }
        private static ExtractorItemInfo GetExtractorItem(string parameterType)
        {
            if (extractorItems != null)
            {
                return InnerGetExtractorItem(parameterType);
            }
            else
            {
                ReadConfigItems();
                return InnerGetExtractorItem(parameterType);
            }
        }

        private static void ReadConfigItems()
        {
            string fileRootDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BusinessTemplates");
            string tempalteFileName = "BusinessInfoExtractConfig.xml";
            string fileFullPath = Path.Combine(fileRootDirectory, tempalteFileName);
            XDocument document = XDocument.Load(fileFullPath);
            var elements = document.Element(XName.Get("Items")).Elements().ToList();
            extractorItems = (from element in elements
                              select new ExtractorItemInfo
                                  {
                                      ParameterType = element.Attribute("ParameterType").Value,
                                      ExtractorType = element.Attribute("ExtractorType").Value,
                                      CustomColumnGetterType = element.Attribute("CustomColumnGetterType").Value,
                                      QueryCriteriaGetterType = element.Attribute("QueryCriteriaGetterType").Value,
                                      QueryServiceGetterType = element.Attribute("QueryServiceGetterType").Value,
                                      DataBinderType = element.Attribute("DataBinderType").Value,
                                      PostHandlerType = element.Attribute("PostHandlerType").Value,
                                      SpecialConditionerType = element.Attribute("SpecialConditionerType").Value
                                  }).ToList();
        }
        private static ExtractorItemInfo InnerGetExtractorItem(string parameterType)
        {
            return extractorItems.Find(item => item.ParameterType == parameterType);
        }

        private class ExtractorItemInfo
        {
            public string ParameterType
            {
                get;
                set;
            }
            public string ExtractorType
            {
                get;
                set;
            }
            public string CustomColumnGetterType
            {
                get;
                set;
            }
            public string QueryCriteriaGetterType
            {
                get;
                set;
            }
            public string QueryServiceGetterType
            {
                get;
                set;
            }
            public string DataBinderType
            {
                get;
                set;
            }
            public string PostHandlerType
            {
                get;
                set;
            }

            public string SpecialConditionerType { get; set; }
        }
    }
}
